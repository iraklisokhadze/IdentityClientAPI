using AutoMapper;
using IdentityClient.API.Validators;
using IdentityClient.Core.Models;
using IdentityClient.Core.Models.RequestModels;
using IdentityClient.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityClient.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService,
                              IAddressService addressService,
                              IMapper mapper)
        {
            _userService = userService;
            _addressService = addressService;
            _mapper = mapper;
        }

        /// <summary>
        /// მომხმარებლის რეგისტრაცია
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Regsister([FromBody] RegisterUserRequest request)
        {
            var userReq = _mapper.Map<User>(request);
            var validationResult = UserRequestValidator.Instance.ValidateRequest(userReq);
            if (validationResult != null)
                return ValidationProblem(validationResult);

            await _userService.Create(userReq);
            userReq.ResidentialAddress.UserId = userReq.Id;
            await _addressService.Create(userReq.ResidentialAddress);
            return Created("", userReq);
        }

        /// <summary>
        /// ავტორიზაცია
        /// </summary>
        /// <param name="userName">მომხმარებლის სახელი</param>
        /// <param name="password">მომხმარებლის პაროლი</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> LogIn(string userName, string password) => await _userService.LogIn(userName, password);

    }


}
