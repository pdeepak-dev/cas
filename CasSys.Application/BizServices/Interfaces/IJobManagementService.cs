using System.Threading.Tasks;
using CasSys.Application.Dtos;
using System.Collections.Generic;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Domain.EntityFrameworkCore.Collections;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IJobManagementService
    {
        Task<OperationResult<IPagedList<JobDto>>> GetAllJobsAsync(int pageIndex, int pageSize);
        Task<OperationResult<JobDto>> GetJobByIdAsync(int id);
        Task<OperationResult<IPagedList<JobDto>>> GetAllJobsByUserIdAsync(int pageIndex, int pageSize);

        Task<OperationResult<JobDto>> CreateJobAsync(JobRequestModel model);
        Task<OperationResult> UpdateJobAsync(JobUpdateRequestModel model);

        OperationResult DeleteJob(int id);
        OperationResult DeleteJob(JobBaseRequestModel model);

        OperationResult MarkJobAsFilled(int jobId);

        Task<OperationResult> ApplyForJob(ApplicantRequestModel model);

        Task<OperationResult<JobApplicantDto>> GetApplicantsByJobIdAsync(int jobId);
        Task<OperationResult<IEnumerable<ApplicantDto>>> GetApplicantsByUserIdAsync();
    }
}