using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly GoogleSettings _googleSettings;

        public AuthenticationController(IOptions<GoogleSettings> googleSettings)
        {
            _googleSettings = googleSettings.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(AuthRegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(AuthLoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GoogleSignInStart")]
        public IActionResult GoogleSignInStartAsync()
        {
            var clientId = _googleSettings.ClientId;

            var googleAuthorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                $"client_id={clientId}" +
                $"response_type=code&" +
                $"scope=openid%20email%20profile&" +
                $"access_type=offline&" +
                $"redirect_uri=https://localhost:7109/api/Authentication/GoogleSignIn";

            return Redirect(googleAuthorizationUrl);
        }

        [HttpGet("GoogleSignIn")]
        public async Task<IActionResult> GoogleSignInAsync(string code, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
