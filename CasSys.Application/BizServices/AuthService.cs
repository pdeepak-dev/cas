using AutoMapper;
using System.Threading.Tasks;
using CasSys.Application.Jwt;
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

        private readonly IJwtFactory _jwtFactory;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtFactory jwtFactory, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._jwtFactory = jwtFactory;
        }

        public async Task<OperationResult<AccessToken>> Authenticate(AuthenticateRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var authenticateResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (authenticateResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = Task.Run(() => _jwtFactory.GenerateEncodedToken(new JwtUserResource
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        Email = user.Email,
                        Roles = roles,
                        IsThirdPartyClient = user.IsThirdPartyClient.HasValue ? user.IsThirdPartyClient.Value : false
                    })).Result;

                    return OperationResult<AccessToken>.Success(jwtToken);
                }
            }

            return OperationResult<AccessToken>.Failed(new[] { "Invalid username or password" });
        }
    }
}
