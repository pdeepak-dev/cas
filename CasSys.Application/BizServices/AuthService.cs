using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CasSys.Domain.Entities.Identity;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;

namespace CasSys.Application.BizServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public async Task<OperationResult> Validate(AuthenticateRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var authenticateResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (authenticateResult.Succeeded)
                {
                    return OperationResult.Success;
                }
            }

            return OperationResult.Failed(new[] { "Invalid username or password" });
        }
    }
}
