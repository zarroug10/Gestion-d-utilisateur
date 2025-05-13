using System;
using System.Text.Encodings.Web;

using Auto_Circuit.DTO;
using Auto_Circuit.DTOs;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Interfaces;
using Auto_Circuit.Services;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly EmailSenderService _emailSenderService;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    public AuthenticationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        EmailSenderService emailSenderService,
        ICurrentUser currentUser,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSenderService = emailSenderService;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> SignUp(UserSignUpDto signUpDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(signUpDto);
        user.Id = Guid.NewGuid().ToString();
        user.ContractId.OwnerId = _currentUser.UserId;

        var result = await _userManager.CreateAsync(user, signUpDto.Password);

        if (result.Succeeded)
        {
            // Optional: assign role if needed and it exists
            //fix the userRole 
            if (signUpDto.Role == null)
            {
                await _userManager.AddToRoleAsync(user, UserType.Directeur);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, signUpDto.Role);
            }

            try
            {
                await SendConfirmationEmail(signUpDto.Email, user);
            }
            catch (Exception ex)
            {
                // Optionally log the error here
                return StatusCode(500, "User created, but failed to send confirmation email.");
            }

            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
            });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }

    private async Task SendConfirmationEmail(string email, User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confiramtionLink = Url.Action(
            "ConfirmEmail",
            "Authentication",
            new { userId = user.Id, token },
            protocol: HttpContext.Request.Scheme);

        //Encode the link to prevent XSS and other injection attacks
        var safeLink = HtmlEncoder.Default.Encode(confiramtionLink);

        var subject = "Formation Authentifcation Email";

        var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Hi {user.UserName}</p>
            <p>You has been accepted to Work with Us.
             please confirm your email address by clicking the button below:</p>
            <p>
                <a href=""{safeLink}"" 
                   style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                          font-weight:bold;border-radius:5px;display:inline-block;"">
                    Confirm Email
                </a>
            </p>
            <p>If the button doesn’t work for you, copy and paste the following URL into your browser:
                <br />
                <a href=""{safeLink}"" style=""color:#007bff;text-decoration:none;"">{safeLink}</a>
            </p>
            <p>If you did not sign up for this account, please ignore this email.</p>
            <p>Thanks,<br />
            The Recruitment Team</p>
        </div>
    ";
        await _emailSenderService.SendEmailAync(email, subject, messageBody, true);
    }

    [HttpGet("confirm-Email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
    {
        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Token))
        {
            return BadRequest("The link is invalid or has expired. Please request a new one if needed.");
        }

        var user = await _userManager.FindByIdAsync(UserId);
        if (user == null)
        {
            return NotFound("We could not find a user associated with the given link.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, Token);
        if (result.Succeeded)
        {
            return Ok("Thank you for confirming your email address. Your account is now verified!");
        }

        return BadRequest("We were unable to confirm your email address. Please try again or request a new link.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTo loginDTo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByNameAsync(loginDTo.Username ?? string.Empty) ??
                   await _userManager.FindByEmailAsync(loginDTo.Email ?? string.Empty);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }

        var validPassword = await _userManager.CheckPasswordAsync(user, loginDTo.Password);

        if (!validPassword)
        {
            ModelState.AddModelError(string.Empty, "Incorrect password.");
            return BadRequest(ModelState);
        }

        if (!user.EmailConfirmed)
        {
            ModelState.AddModelError(string.Empty, "Your email address is not confirmed. " +
                "Please confirm your email before logging in.");
            return BadRequest(ModelState);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTo.Password, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var userProperties = user.GetType().GetProperties();
            foreach (var prop in userProperties)
            {
                var value = prop.GetValue(user);
                if (value == null)
                {
                    Console.WriteLine($"{prop.Name} is null");
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: loginDTo.RememberMe);
            return Ok(new { Success = true });
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
    }

}
