using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Weather.Models;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public UsersController(SignInManager<IdentityUser> signinManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterApiModel form)
        {
            var identityUser_withNameAsEmail = new IdentityUser
            {
                UserName = form.Email,
                Email = form.Email
            };

            var result = await _userManager.CreateAsync(identityUser_withNameAsEmail, form.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser_withNameAsEmail);

                //var callbackUrl = Url.Content($"~/users/{identityUser_withNameAsEmail.Id}/email/confirm?code={code}");
                var callbackUrl = Url.Action("ConfirmEmail", "Users", new { userId = identityUser_withNameAsEmail.Id, code }, protocol: HttpContext.Request.Scheme);
                var message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                //you can send the email here

                return Accepted("", new { message });
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginApiModel model)
        {
            var result = await _signinManager.PasswordSignInAsync(model.Email,
                    model.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var identityUserToLogin = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateTokenAsync(identityUserToLogin, _jwtOptions.SecurityKey);
                return Ok(new { message = "You have successfully logged-in!", token });
            }

            return BadRequest();
        }

        [HttpGet("{userId}/email/confirm")]
        public async Task<ActionResult> ConfirmEmail(string userId, [FromQuery] string code)
        {
            if (userId == null || code == null)
                return BadRequest();
            
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
                return BadRequest();
            
            var result = await _userManager.ConfirmEmailAsync(user, code);
            
            if (result.Succeeded)
                return Ok(new { message = "Your email is confirmed; you may now login your registered credentials" });
            else
                return BadRequest();
        }

        private string GenerateTokenAsync(IdentityUser user, SecurityKey securityKey)
        {
            IList<Claim> userClaims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim("Email", user.Email)
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            ));
        }
    }
}
