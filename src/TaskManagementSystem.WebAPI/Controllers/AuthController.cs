using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dto.Auth;
using TaskManagementSystem.Domain.Interfaces.Services.Auth;

namespace TaskManagementSystem.WebAPI.Controllers;

[ApiController]
[Route("api")]
[AllowAnonymous]
public class AuthController(IRegisterService registerService, ILoginService loginService, IPasswordService passwordService)
  : ControllerBase
{
  [HttpPost("register")]
  [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
  public async Task<ActionResult<RegisterResponse>> Register([FromBody] CreateUserRequest request, CancellationToken ct)
  {
    var jwtTokenResult = await registerService.RegisterNewUser(
      request.Age,
      request.FirstName,
      request.LastName,
      request.Email,
      request.Password);

    return jwtTokenResult.IsSuccess
      ? Ok(new RegisterResponse (jwtTokenResult.Value))
      : BadRequest("Error creating new user!");
  }

  // POST: api/login
  [HttpPost("login")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
  public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken ct)
  {
    var jwtTokenResult = await loginService.LoginAsync(request.Email, request.Password);

    return jwtTokenResult.IsSuccess
      ? Ok(new LoginResponse (jwtTokenResult.Value ))
      : BadRequest("Invalid credentials!");
  }
  
  // PUT: api/auth/password
  [Authorize]
  [HttpPut("auth/update-password")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request, CancellationToken ct)
  {
    var result = await passwordService.UpdatePasswordAsync(request.UserId, request.NewPassword);

    return result.Status switch
    {
      ResultStatus.Ok => Ok(),
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Error => StatusCode(StatusCodes.Status400BadRequest, result.Errors.FirstOrDefault()),
      _ => StatusCode(StatusCodes.Status400BadRequest)
    };

  }
}
