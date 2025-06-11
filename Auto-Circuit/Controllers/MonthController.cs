using Auto_Circuit.Data.Repository;
using Auto_Circuit.DTO;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonthController(MonthlySpentRepository monthly) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllData()
    {
        try
        {
            var SpentList = await monthly.GetSpent();
            return Ok(SpentList);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpentById(string id)
    {
        try
        {
            var spent = await monthly.GetSpentById(id);
            if (spent == null)
            {
                return NotFound("No Record Has been found");
            }
            else
            {
                return Ok(spent);
            }
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateSpent(string id, MonthlySpentDTo spentDTo)
    {
        var updatedspent = await monthly.UpdateSpent(id, spentDTo);

        return Ok(updatedspent);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteSpent(string id)
    {
        try
        {
            await monthly.DeleteSpent(id);
            return Ok(new { message = "Deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(MonthlySpentDTo spentDTo)
    {
        try
        {
            return Ok(await monthly.CreateSpent(spentDTo));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
