using System.Threading.Tasks;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Application.Jwt;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IAuthService
    {
        Task<OperationResult> Validate(AuthenticateRequestModel model);
        Task<OperationResult<AccessToken>> Authenticate(AuthenticateRequestModel authenticateRequestModel);
    }
}