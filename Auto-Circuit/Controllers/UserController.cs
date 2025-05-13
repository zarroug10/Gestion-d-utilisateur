using System.Text.Encodings.Web;

using Auto_Circuit.Data;
using Auto_Circuit.Data.Repository;
using Auto_Circuit.DTO;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Interfaces;
using Auto_Circuit.Services;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly CircuitContext _dbContext;

    public UserController(
        UserRepository userRepository,
        UserManager<User> userManager,
        EmailSenderService emailSenderService,
        IMapper mapper,
        ICurrentUser currentUser,
        CircuitContext circuitContext)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
        _currentUser = currentUser;
        _dbContext = circuitContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllUsersAsync();

        return Ok(users);
    }

    [HttpGet("CurrentUser")]
    public IActionResult GetCurrentUser() => Ok(_currentUser.User);

    [HttpPatch("Update")]
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
            _mapper.Map(updateDTo, user);
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
            var contracts = _dbContext.Contracts.Where(c => c.UserId == id);
            _dbContext.Contracts.RemoveRange(contracts);
            await _dbContext.SaveChangesAsync();
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}