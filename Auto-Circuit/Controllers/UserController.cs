using System.Text.Encodings.Web;

using Auto_Circuit.Data.Repository;
using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly EmailSenderService _emailSenderService;

    public UserController(
        UserRepository userRepository,
        UserManager<User> userManager,
        EmailSenderService emailSenderService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _emailSenderService = emailSenderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }




    [HttpPatch("Update/{id}")]
    public async Task<IActionResult> UpdateUser(UpdateDTo updateDTo, [FromQuery] string id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = updateDTo.UserName;
            user.Email = updateDTo.Email;
            user.FirstName = updateDTo.FirstName;
            user.LastName = updateDTo.LastName;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteUser([FromQuery] string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}