using Auto_Circuit.Data.Repository;
using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacationController(
    ICurrentUser currentUser,
    VacationRepository vacationRepository
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var vacations = await vacationRepository.GetAllAsync();
        return Ok(vacations);
    }

    [HttpGet("{user}")]
    public async Task<IActionResult> GetVacationByUserAsync(string user)
    {
        var vacations = await vacationRepository.GetVacationsbyUserIdAsync(id ?? currentUser.UserId);
        return Ok(vacations);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateVacation(VacationFormDTo vacation)
    {
        var createdVacation = await vacationRepository.CreateVacationAsync(vacation);
        return Ok(createdVacation);
    }
}
