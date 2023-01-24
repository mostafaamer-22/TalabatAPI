using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Services;
using Talabat.DAL.Entities.Identity;
using Talabat.Dtos;
using Talabat.Extensions;

namespace Talabat.Controllers
{
    public class AccountController : BaseControllerApi
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager ,
            SignInManager<AppUser> signInManager , ITokenService tokenService , IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
         
            if(CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest();
            }

            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0],
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = new Address()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    City = registerDto.City,
                    Country = registerDto.Country,
                    Street = registerDto.Street,
                    ZipCode = registerDto.ZipCode
                },
                
                
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
                return BadRequest();
            var userDto = new UserDto()
            {
                Email = registerDto.Email,
                DisplayName = $"{user.DisplayName}",
                Token = await _tokenService.CreateToken(user, _userManager)
            };
            return Ok(userDto);
           
        }

        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
            => await _userManager.FindByEmailAsync(email) != null;

        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           var user = await _userManager.FindByEmailAsync(loginDto.Email);  

            if(user == null)
                return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(user , loginDto.Password , false);
            if (!result.Succeeded)
                return Unauthorized();

            var userDto = new UserDto()
            {
                Email = loginDto.Email,
                DisplayName = $"{user.DisplayName}",
                Token = await _tokenService.CreateToken(user, _userManager)
            };
            return Ok(userDto);


        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            }
            );
        }

        [Authorize]
        [HttpGet("GetUserAddress")]

        public async Task<ActionResult<UserAddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            var address = _mapper.Map<Address, UserAddressDto>(user.Address);
            return Ok(address);
        }



        [Authorize]
        [HttpPut("UpdateUserAddress")]
        public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            var address = _mapper.Map<UserAddressDto, Address>(addressDto);
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(_mapper.Map<Address, UserAddressDto>(user.Address));

            return BadRequest();
        }


    }
}
