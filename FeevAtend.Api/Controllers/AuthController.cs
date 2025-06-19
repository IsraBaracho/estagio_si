using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeevAtend.Application.DTOs;
using FeevAtend.Domain.Entities;
using FeevAtend.Application.Services;

namespace FeevAtend.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] FeevAtend.Application.DTOs.LoginRequest request)
    {
        var user = await _userService.GetUserByEmail(request.Email);
        if (user == null || !_userService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { error = "Invalid credentials" });
        }

        var token = _authService.GenerateJwtToken(user);
        return Ok(new LoginResponse { Token = token });
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] FeevAtend.Application.DTOs.RegisterRequest request)
    {
        var user = await _userService.CreateUser(request);
        return Ok(new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        });
    }
}

