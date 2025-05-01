using AutoMapper;
using Domain.Entites;
using Domain.Excaptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{


	public class AuthenticationService(UserManager<User> userManager, IMapper mapper,IOptions<JwtOptions> options) : IAuthenticationServices
	{
		public async Task<AddressDtos> GetUserAddressAsync(string email)
		{
			var user = await _userManager.Users.Include(x => x.Address)
				.FirstOrDefaultAsync(x => x.Email == email);
			if (user is null)
				throw new UserNotFoundException(email);

			return _mapper.Map<AddressDtos>(user.Address);
		}

		public async Task<UserResultDto> GetUserByEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null)
				throw new UserNotFoundException(email);

			return new UserResultDto(
				user.DisplayName,
				user.Email,
				null
			);
		}

		public async Task<bool> IsEmailExist(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			return user != null;
		}

		public async Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto)
		{
			var user = await userManager.Users.Include(x => x.Address)
				.FirstOrDefaultAsync(x => x.Email == email);
			if (user is null)
				throw new UserNotFoundException(email);

			if (user.Address != null)
			{
				user.Address.FirstName = addressDto.FirstName;
				user.Address.LastName = addressDto.LastName;
				user.Address.Street = addressDto.Street;
				user.Address.City = addressDto.City;
				user.Address.Country = addressDto.Country;
			}
			else
			{
				var mappedAddress = _mapper.Map<Address>(addressDto);
				user.Address = mappedAddress;
			}

			await userManager.UpdateAsync(user);
			return addressDto;
		}


		public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);

			if (user is null)
			{
				throw new UnauthorizedAccessException($"Email: {loginDto.Email} does not exist!");
			}

			var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

			if (!result)
			{
				throw new UnauthorizedAccessException();
			}

			return new UserResultDto
			(
				 user.DisplayName,
				  user.Email,
				 "TOKEN"
			);
		}

		public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
		{
			var user = new User
			{
				Email = registerDto.Email,
				DisplayName = registerDto.DisplayName,
				UserName = registerDto.UserName,
				PhoneNumber = registerDto.PhoneNumber
			};

			var result = await userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(x => x.Description).ToList();
				throw new ValidationException(errors);
			}

			return new UserResultDto
			(
				 user.DisplayName,
				  user.Email,
				 "TOKEN"
			);

		}


		private async Task<string> CreateTokenAsync(User user)
		{
			var JwtOptions = options.Value;
			
			// Claims
			var claims = new List<Claim>
	        {
		     new Claim(ClaimTypes.Name, user.UserName),
		     new Claim(ClaimTypes.Email, user.Email)
	        };

			var roles = await userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("top-secret-key"));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
	          claims: claims,
	          signingCredentials: creds,
	         expires: DateTime.UtcNow.AddDays(JwtOptions.DurationInDays),
	         audience: ""
              );

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
























}
	
