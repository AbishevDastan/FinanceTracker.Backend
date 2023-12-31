﻿using Application.Extensions.UserContext;
using Application.Services.UserService;
using Application.UseCases.User;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();

            if (users == null || users.Count == 0)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var users = await _userService.GetUser(userId);

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<int>> Register(RegisterUserDto request)
        {
            var response = await _userService.Register(
                new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    CreationDate = DateTime.Now,
                }, request.Password);

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenModel>> Login(AuthenticateUserDto request)
        {
            var response = await _userService.Login(request.Email, request.Password);
            return Ok(response);
        }

        //[HttpGet("{searchText}/search")]
        //public async Task<ActionResult<List<UserDto>>> SearchUsers(string searchText)
        //{
        //    var result = await _userService.SearchUsers(searchText);

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result);
        //}
    }
}
