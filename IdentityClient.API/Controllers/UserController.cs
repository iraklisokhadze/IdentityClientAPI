using AutoMapper;
using IdentityClient.API.Validators;
using IdentityClient.Core.Models;
using IdentityClient.Core.Models.RequestModels;
using IdentityClient.Core.Models.ResponseModels;
using IdentityClient.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace IdentityClient.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IAddressService addressService, IMapper mapper)
        {
            _userService = userService;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(EditUserRequest user)
        {
            var userReq = _mapper.Map<User>(user);
            userReq.Deleted = true;
            await _userService.Edit(userReq);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserData(EditUserRequest user)
        {
            var userReq = _mapper.Map<User>(user);

            var validationResult = UserRequestValidator.Instance.ValidateRequest(userReq);
            if (validationResult != null)
                return ValidationProblem(validationResult);


            await _userService.Edit(userReq);
            await _addressService.Edit(userReq.ResidentialAddress);
            return Ok();
        }
    }
}
