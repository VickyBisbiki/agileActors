using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_Aggregation.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Aggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("User name cannot be null or empty.");
            }

            try
            {
                var repositories = await _gitHubService.GetUserAsync(userName);


                if (repositories == null)
                {
                    return NotFound($"GitHub user '{userName}' not found.");
                }

                return Ok(repositories);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }

}

