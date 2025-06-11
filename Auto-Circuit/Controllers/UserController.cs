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

    [HttpGet("All")]
    public async Task<IActionResult> GetAll(string? search)
    {
        try
        {
            var users = await _userRepository.GetAllUsersAsync(search);

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id ?? _currentUser.UserId);
        return Ok(user);
    }

    [HttpGet("CurrentUser")]
    public IActionResult GetCurrentUser() => Ok(_currentUser.User);

    [HttpPatch("Update/{id?}")]
    public async Task<IActionResult> UpdateUser(UpdateDTo updateDTo, string? id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(id ?? _currentUser.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var contracts = await _dbContext.Contracts.Where(c => c.UserId == id).FirstOrDefaultAsync();

            _mapper.Map(updateDTo, user);
            _mapper.Map(updateDTo.ContractDto, contracts);

            var user21 = await _userManager.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("Delete/{id?}")]
    public async Task<IActionResult> DeleteUser(string? id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var contracts = _dbContext.Contracts.Where(c => c.UserId == id);
            var vacation = _dbContext.Vacations.Where(c => c.UserId == id);
            _dbContext.Contracts.RemoveRange(contracts);
            _dbContext.Vacations.RemoveRange(vacation);
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