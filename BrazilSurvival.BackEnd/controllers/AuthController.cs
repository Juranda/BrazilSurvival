using BrazilSurvival.BackEnd.Controllers.CustomActionFilters;
using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [Route("/login")]
        [ValidateModel]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Console.WriteLine(request.ToString());

            return Ok();
        }
    }
}
