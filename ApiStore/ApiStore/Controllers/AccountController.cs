using ApiStore.Constants;
using ApiStore.Data.Entities.Identity;
using ApiStore.Interfaces;
using ApiStore.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private IJwtTokenService _jwtTokenService;
        private IImageTool _imageTool;
        private IMapper _mapper;
        public AuthController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService,
            IImageTool imageTool, IMapper mapper)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _imageTool = imageTool;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                    return BadRequest("Invalid data");

                var token = await _jwtTokenService.GenerateTokenAsync(user);
                return Ok(new { token, user.FirstName, user.Email, user.LastName, user.Image });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                string image = string.Empty;
                if (!(string.IsNullOrEmpty(model.Image)))
                    image = await _imageTool.SaveImageByUrl(model.Image);

                var user = _mapper.Map<UserEntity>(model);
                user.Image = image;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) result = await _userManager.AddToRoleAsync(user, Roles.User);
                else return BadRequest("Smth went wrong!");

                var token = await _jwtTokenService.GenerateTokenAsync(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}