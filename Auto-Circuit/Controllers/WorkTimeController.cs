using Auto_Circuit.Data.Repository;
using Auto_Circuit.DTO;
using Auto_Circuit.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Auto_Circuit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkTimeController(WorkTimeRepository workTimeRepository, ICurrentUser currentUser) : ControllerBase
{
    [HttpGet("User/Work")]
    public async Task<IActionResult> GetWorkTimeAsync()
    {
        try
        {
            var workTime = await workTimeRepository.GetWorkTimeByUserAsync();
            return Ok(workTime);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllWorkTime()
    {
        try
        {
            var workTime = await workTimeRepository.GetAllWorkTime();
            return Ok(workTime);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id?}")]
    public async Task<IActionResult> GetWorkTimeById(string? id)
    {
        try
        {
            var workTime = await workTimeRepository.GetWorkTimeById(id);
            if (workTime == null)
            {
                return NotFound("No Record Has been found");
            }
            else
            {
                return Ok(workTime);
            }
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("User/{UserId?}")]
    public async Task<IActionResult> GetWorkTimeByUser(string? UserId)
    {
        try
        {
            var workTimes = await workTimeRepository.GetWorkTimeByUser(UserId);
            return Ok(workTimes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkTime(WorkTimeRequest workTimeDTo)
    {
        try
        {
            if (currentUser.UserId != null)
            {
                workTimeDTo.UserId = currentUser.UserId;
                await workTimeRepository.AddWorkTime(workTime: workTimeDTo);
                return Ok(new { message = "WorkTime Created successfully." });
            }
            else
            {
                return Unauthorized("You Must be logged in ");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateWorkTime(WorkTimeDTo workTimeDTo, string? id)
    {
        try
        {
            await workTimeRepository.UpdateWorkTime(workTimeDTo, id);
            return Ok("WorkTime updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteWorkTime(string id)
    {
        try
        {
            await workTimeRepository.DeleteWorkTime(id);
            return Ok(new { message = "WorkTime deleted successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/approve/{id}")]
    public async Task<IActionResult> approveWorkTime(string? id)
    {
        try
        {
            await workTimeRepository.ApproveWorkTime(id);
            return Ok(new { message = "WorkTime updated successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("update/reject/{id}")]
    public async Task<IActionResult> rejecteWorkTime(string? id)
    {
        try
        {
            await workTimeRepository.rejectWorkTime(id);
            return Ok(new { message = "WorkTime updated successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
