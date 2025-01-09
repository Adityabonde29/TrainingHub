using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using dotnetapp.Exceptions;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalTrainingController : ControllerBase
    {
        private readonly PhysicalTrainingService _physicalTrainingService;

        public PhysicalTrainingController(PhysicalTrainingService physicalTrainingService)
        {
            _physicalTrainingService = physicalTrainingService;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<PhysicalTraining>>> GetAllPhysicalTrainings()
        {
            try
            {
                var trainings = await _physicalTrainingService.GetAllPhysicalTrainings();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{trainingId}")]
        [Authorize]

        public async Task<ActionResult<PhysicalTraining>> GetPhysicalTrainingById(int trainingId)
        {
            try
            {
                var training = await _physicalTrainingService.GetPhysicalTrainingById(trainingId);
                if (training == null)
                {
                    return NotFound(new { message = "Cannot find any training." });
                }
                return Ok(training);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult> AddPhysicalTraining([FromBody] PhysicalTraining training)
        {
            try
            {
                var result = await _physicalTrainingService.AddPhysicalTraining(training);
                if (result)
                {
                    return Ok(new { message = "Physical training added successfully." });
                }
                return StatusCode(500, new { message = "Failed to add physical training." });
            }
            catch (PhysicalTrainingException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to add physical training. {ex.Message}" });
            }
        }

        [HttpPut("{trainingId}")]
        public async Task<ActionResult> UpdatePhysicalTraining(int trainingId, [FromBody] PhysicalTraining training)
        {
            try
            {
                var result = await _physicalTrainingService.UpdatePhysicalTraining(trainingId, training);
                if (result)
                {
                    return Ok(new { message = "Physical training updated successfully." });
                }
                return NotFound(new { message = "Cannot find any training." });
            }
            catch (PhysicalTrainingException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{trainingId}")]
        [Authorize]
        public async Task<ActionResult> DeletePhysicalTraining(int trainingId)
        {
            try
            {
                var result = await _physicalTrainingService.DeletePhysicalTraining(trainingId);
                if (result)
                {
                    return Ok(new { message = "Physical training deleted successfully." });
                }
                return NotFound(new { message = "Cannot find any training." });
            }
            catch (PhysicalTrainingException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
