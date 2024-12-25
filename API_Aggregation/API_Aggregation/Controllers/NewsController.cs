using API_Aggregation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API_Aggregation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{dateFrom}/{sortBy}")]
        public async Task<IActionResult> GetTopNews(string dateFrom, string sortBy)
        {
     

            try
            {
                var response = await _newsService.GetTopHeadlinesNewsAsync(dateFrom, sortBy);


                if (response == null)
                {
                    return NotFound(new { message = $"No data found for those dates: {dateFrom} sprted by {sortBy}" });
                }
                return Ok(response);
               
    
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
