using BookStore.Services.DTO;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Obtém os quantitativos para alimentar o dashboard.
        /// </summary>
        /// <returns></returns>
        [Route("GetDashboard")]
        [HttpGet]
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var dashboard = await _dashboardService.GetDashboard();

            return Ok(dashboard);
        }
    }
}
