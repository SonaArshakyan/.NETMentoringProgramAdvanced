using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagementSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [Authorize(Roles = "Manager")]
        [HttpGet("manager")]
        public IActionResult ManagerData()
        {
            return Ok("Manager data");
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("buyer")]
        public IActionResult BuyerData()
        {
            return Ok("Buyer data");
        }
    }
}
