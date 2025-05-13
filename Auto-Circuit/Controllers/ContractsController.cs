using Auto_Circuit.Data.Repository;
using Auto_Circuit.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController(ContractRepository contractRepository, ICurrentUser currentUser) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await contractRepository.GetContractsAsync());
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetContractsByUser(string? id)
    {
        try
        {
            var contact = await contractRepository.GetContactByUser(id ?? currentUser.UserId);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
