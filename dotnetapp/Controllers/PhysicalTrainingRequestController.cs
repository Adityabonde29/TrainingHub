// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using dotnetapp.Models;
// using dotnetapp.Exceptions;
// using dotnetapp.Services;
// using Microsoft.AspNetCore.Authorization;

// namespace dotnetapp.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class PhysicalTrainingRequestController : ControllerBase
//     {
//         private readonly PhysicalTrainingRequestService _physicalTrainingRequestService;

//         public PhysicalTrainingRequestController(PhysicalTrainingRequestService physicalTrainingRequestService)
//         {
//             _physicalTrainingRequestService = physicalTrainingRequestService;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<PhysicalTrainingRequest>>> GetAllPhysicalTrainingRequests()
//         {
//             var requests = await _physicalTrainingRequestService.GetAllPhysicalTrainingRequests();
//             return Ok(requests);
//         }

//         [HttpGet("user/{userId}")]
//         public async Task<ActionResult<IEnumerable<PhysicalTrainingRequest>>> GetPhysicalTrainingRequestsByUserId(int userId)
//         {
//             var requests = await _physicalTrainingRequestService.GetPhysicalTrainingRequestsByUserId(userId);
//             return Ok(requests);
//         }

//         [HttpPost]
//         public async Task<ActionResult> AddPhysicalTrainingRequest([FromBody] PhysicalTrainingRequest request)
//         {
//             try
//             {
//                 await _physicalTrainingRequestService.AddPhysicalTrainingRequest(request);
//                 return Ok(new { message = "Physical training request added successfully." });
//             }
//             catch (PhysicalTrainingException ex)
//             {
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPut("{requestId}")]
//         public async Task<ActionResult> UpdatePhysicalTrainingRequest(int requestId, [FromBody] PhysicalTrainingRequest request)
//         {
//             try
//             {
//                 var result = await _physicalTrainingRequestService.UpdatePhysicalTrainingRequest(requestId, request);

//                 if (result)
//                 {
//                     return Ok(new { message = "Physical training request updated successfully." });
//                 }
//                 else
//                 {
//                     return NotFound(new { message = "Cannot find the request." });
//                 }
//             }
//             catch (PhysicalTrainingException ex)
//             {
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpDelete("{requestId}")]
//         public async Task<ActionResult> DeletePhysicalTrainingRequest(int requestId)
//         {
//             try
//             {
//                 var result = await _physicalTrainingRequestService.DeletePhysicalTrainingRequest(requestId);
//                 if (!result)
//                 {
//                     return NotFound(new { message = "Cannot find the request." });
//                 }
//                 return Ok(new { message = "Physical training request deleted successfully." });
//             }
//             catch (PhysicalTrainingException ex)
//             {
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using dotnetapp.Exceptions;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
 
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalTrainingRequestController : ControllerBase
    {
        private readonly PhysicalTrainingRequestService _physicalTrainingRequestService;
 
        public PhysicalTrainingRequestController(PhysicalTrainingRequestService physicalTrainingRequestService)
        {
            _physicalTrainingRequestService = physicalTrainingRequestService;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalTrainingRequest>>> GetAllPhysicalTrainingRequests()
        {
            var requests = await _physicalTrainingRequestService.GetAllPhysicalTrainingRequests();
            return Ok(requests);
        }
 
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PhysicalTrainingRequest>>> GetPhysicalTrainingRequestsByUserId(int userId)
        {
            var requests = await _physicalTrainingRequestService.GetPhysicalTrainingRequestsByUserId(userId);
            return Ok(requests);
        }
 
        [HttpPost]
        public async Task<ActionResult> AddPhysicalTrainingRequest([FromBody] PhysicalTrainingRequest request)
        {
            try
            {
                await _physicalTrainingRequestService.AddPhysicalTrainingRequest(request);
                return Ok(new { message = "Physical training request added successfully." });
            }
            catch (PhysicalTrainingException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
 
        [HttpPut("{requestId}")]
        public async Task<ActionResult> UpdatePhysicalTrainingRequest(int requestId, [FromBody] PhysicalTrainingRequest request)
        {
            try
            {
                var result = await _physicalTrainingRequestService.UpdatePhysicalTrainingRequest(requestId, request);
 
                if (result)
                {
                    return Ok(new { message = "Physical training request updated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Cannot find the request." });
                }
            }
            catch (PhysicalTrainingException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
 
        [HttpDelete("{requestId}")]
        public async Task<ActionResult> DeletePhysicalTrainingRequest(int requestId)
        {
            try
            {
                var result = await _physicalTrainingRequestService.DeletePhysicalTrainingRequest(requestId);
                if (!result)
                {
                    return NotFound(new { message = "Cannot find the request." });
                }
                return Ok(new { message = "Physical training request deleted successfully." });
            }
            catch (PhysicalTrainingException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}