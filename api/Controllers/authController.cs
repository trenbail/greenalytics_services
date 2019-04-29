using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using api.domain;
using api.repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthRepository AuthRepository { get; }

        public AuthController(IAuthRepository authRepository)
        {
            AuthRepository = authRepository;
        }

        [HttpPost("login/{userID}/{password}/{token}")]
        public string login(string userID, string password, string token)
        {
            return this.AuthRepository.login(userID, password, token);
        }

        [HttpPost("signup/{userID}/{password}")]
        public void signup(string userID, string password)
        {
            AuthRepository.signup(userID, password);
        }
}
}
