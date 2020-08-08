using System.Threading.Tasks;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IAuthService
    {
        Task<OperationResult> Validate(AuthenticateRequestModel model);
    }
}