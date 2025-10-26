using BusinessLayer.Services;
using DataLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace InvoiceGenAppAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    
    
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public AccountController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpPut("AddCompany")]

        public async Task<IActionResult> AddCompany([FromBody]CompanyDTO company)
        {
            try
            {
                var result =  await _userService.AddCompany(company.UserId, company.Text);

                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch(Exception ex) { 
             return BadRequest(ex.Message);
            }
        }
       
        
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var result = await _userService.GetUserById(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string UserName)
        {
            try
            {
                var result = await _userService.GetUserByUserName(UserName);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("UpdateUser")]
        public async Task<IActionResult> Update(UserRequestDTO userRequestDTO,string userID)
        {
            try
            {
                var result = await _userService.Update(userRequestDTO, userID);
                return Ok(result);
            }
            catch(Exception ex)
            {
               return BadRequest($"Error: {ex.Message}");
            }
            
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetallUser")]

        public async Task<IActionResult> GellAllUser()
        {
            try
            {
                var result = await _userService.GetUser();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
