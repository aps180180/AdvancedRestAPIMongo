﻿using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;



namespace AdvancedRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser _userService;
        public UsersController( IUser userService)
        {
                _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAllUsers();
            if (result.isSuccess)
            {
                return Ok(result.User);
            }
            return NotFound(result.ErrorMessage);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetUserById(id);
            if (result.isSuccess)
            {
                return Ok(result.User);
            }
            return NotFound(result.ErrorMessage);

        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var result= await _userService.AddUser(user);
            if (result.isSuccess) 
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(result.ErrorMessage);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserDTO user)
        {
            var result =  await _userService.UpdateUser(id, user);
            if (result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUser(id);
            if(result.isSuccess) 
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
