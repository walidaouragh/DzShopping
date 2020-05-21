using AutoMapper;
using DzShopping.API.Dtos;
using DzShopping.API.Errors;
using DzShopping.API.Extensions;
using DzShopping.Core.Models.Identity;
using DzShopping.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DzShopping.API.Controllers.AccountController
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        // I used extentions,can use accountService implementation as well!
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<userDto>> GetCurrentUser()
        {
            var user = await _userManager.FindEmailFromClaimsPrinciple(HttpContext.User);

            return Ok(new userDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateJwtToken(user)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {

            var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Problem updating the user");
            }

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<userDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return Ok(new userDto() { 
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateJwtToken(user)
            });
        } 

        [HttpPost("register")]
        public async Task<ActionResult<userDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse()
                {
                    Errors = new[]
                    { $"{registerDto.Email} already exists" }
                });
             }

            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new userDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateJwtToken(user)
            });
        }

    }
}
