using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using GNB.ProductManager.Helpers;
using GNB.ProductManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GNB.ProductManager.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly IAuthenticationService authenticationService;
        private readonly IConfiguration configuration;
        private readonly ILoggerService loggerService;
        public LoginController(IAuthenticationService authenticationService, IConfiguration configuration, ILoggerService loggerService) {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
            this.loggerService = loggerService;
        }
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtTokenResponse), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Get(LoginModel loginModel) {
            if (!authenticationService.AuthenticateUser(new Login { Username = loginModel.Username, Password = loginModel.Password })) {
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Error,
                    Message = $"Usuario {loginModel.Username} no pudo se autenticado con la información proporcionada.",
                    StackTrace = "LoginController.Get"
                });
                return Unauthorized();
            }
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: "Gloiath",
                audience: "Gloiath",
                claims: new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.UniqueName, loginModel.Username)
                },
                expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("Token:ExpiresTime")),
                signingCredentials: new SigningCredentials(JwtSecurityKeyGenerator.Get(configuration.GetValue<string>("Token:Secret")), SecurityAlgorithms.HmacSha256));
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            loggerService.Log(new Log {
                LogType = Domain.ValueObject.LogType.Information,
                Message = $"El token fue generado con éxito conteniendo los Claims especificados.",
                StackTrace = "LoginController.Get"
            });
            return Ok(new JwtTokenResponse() {
                Value = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken),
                ValidTo = jwtSecurityToken.ValidTo
            });
        }
    }
}