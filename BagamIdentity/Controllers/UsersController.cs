using BagamIdentity.Entities;
using BagamIdentity.Models;
using BagamIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BagamIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            var user = _userService.Authenticate(request.Username, request.Password);
            return user == null ? NotFound() : Ok(user);

        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var currentUserId = Guid.Parse(User.Identity.Name);
            if(id!=currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser(User user)
        {
            var user_created = _userService.AddUser(user);
            return user_created == null ? BadRequest() : Ok(user_created);
        }
    }
}
