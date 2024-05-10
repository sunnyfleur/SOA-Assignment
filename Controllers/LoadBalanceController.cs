using SOA_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using SOA_Assignment.Services;

namespace SOA_Assignment.Models
{
    [ApiController]
    [Route("[controller]")]
    public class LoadBalancerController : ControllerBase
    {
        private readonly ILoadBalancerService _loadBalancerService;

        public LoadBalancerController(ILoadBalancerService loadBalancerService)
        {
            _loadBalancerService = loadBalancerService;
        }

        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        public async Task<IActionResult> Proxy()
        {
            var response = await _loadBalancerService.ForwardRequestAsync(Request);
            return Content(response, "application/json");
        }
    }
}