using AutoMapper;
using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.Dtos;
using CasSys.Application.Jwt;
using CasSys.Application.RequestModels;
using CasSys.Domain.Entities;
using CasSys.Domain.Entities.Identity;
using CasSys.Domain.EntityFrameworkCore.Collections;
using CasSys.Domain.EntityFrameworkCore.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CasSys.Application.BizServices
{
    public class JobManagementService : IJobManagementService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        private readonly IMapper _mapper;
        private readonly IJwtHttpContext _jwt;

        public JobManagementService(UserManager<AppUser> userManager, IUnitOfWork uow, IMapper mapper, IJwtHttpContext jwt)
        {
            this._uow = uow;
            this._userManager = userManager;
            this._mapper = mapper;
            this._jwt = jwt;
        }

        public async Task<OperationResult> ApplyForJob(ApplicantRequestModel model)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var _applicantRepo = _uow.GetRepository<Applicant>();

            var jwt = _jwt.GetJwtUserResource();

            // check if user has already applied for job
            var applicantJobStatus = _applicantRepo.GetFirstOrDefault(predicate: x => x.JobId == model.JobId && jwt.Id == x.UserId) != null;

            if (!applicantJobStatus)
            {
                var user = await _userManager.FindByIdAsync(jwt.Id);
                var job = await _jobRepo.GetFirstOrDefaultAsync(x => x.Id == model.JobId);

                if (user != null && job != null)
                {
                    var applicant = new Applicant
                    {
                        JobId = job.Id,
                        UserId = user.Id
                    };

                    try
                    {
                        _applicantRepo.Insert(applicant);
                        await _uow.SaveChangesAsync();

                        return OperationResult.Success;
                    }
                    catch
                    {
                        return OperationResult.Failed(new[] { "An error occurred while applying for job" });
                    }
                }

                return OperationResult.Failed(new[] { "Unable to apply for job" });
            }
            else
            {
                return OperationResult.Failed(new[] { "Already applied for job" });
            }
        }

        public async Task<OperationResult<JobDto>> CreateJobAsync(JobRequestModel model)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var jwt = _jwt.GetJwtUserResource();
            var user = await _userManager.FindByIdAsync(jwt.Id);

            if (user != null)
            {
                var job = _mapper.Map<Job>(model);

                try
                {
                    job.User = user;
                    var savedJob = _jobRepo.Insert(job);
                    await _uow.SaveChangesAsync();

                    return OperationResult<JobDto>.Success(_mapper.Map<JobDto>(savedJob));
                }
                catch
                {
                    return OperationResult<JobDto>.Failed(new[] { $"An error occurred while inserting the job" });
                }
            }
            else
            {
                return OperationResult<JobDto>.Failed(new[] { $"User not found" });
            }
        }

        public OperationResult DeleteJob(int id)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            try
            {
                var job = new Job { Id = id };
                _jobRepo.Delete(job);

                _uow.SaveChanges();

                return OperationResult.Success;
            }
            catch
            {
                return OperationResult<bool>.Failed(new[] { $"An error occurred while deleting the job" });
            }
        }

        public OperationResult DeleteJob(JobBaseRequestModel model)
        {
            return DeleteJob(model.Id);
        }

        public async Task<OperationResult<IPagedList<JobDto>>> GetAllJobsAsync(int pageIndex, int pageSize)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var jobs = await _jobRepo.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);

            return OperationResult<IPagedList<JobDto>>.Success(jobs.ToPagedList(converter: x => _mapper.Map<JobDto>(x)));
        }

        public async Task<OperationResult<IPagedList<JobDto>>> GetAllJobsByUserIdAsync(int pageIndex, int pageSize)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var jwt = _jwt.GetJwtUserResource();

            var jobs = await _jobRepo.GetPagedListAsync(predicate: x => x.UserId == jwt.Id,
                include: x => x.Include(y => y.User).Include(y => y.Applicants).ThenInclude(y => y.User), pageIndex: pageIndex, pageSize: pageSize);

            return OperationResult<IPagedList<JobDto>>.Success(jobs.ToPagedList(converter: x => _mapper.Map<JobDto>(x)));
        }

        public async Task<OperationResult<JobApplicantDto>> GetApplicantsByJobIdAsync(int jobId)
        {
            var _jobRepo = _uow.GetRepository<Job>();
            var _applicantRepo = _uow.GetRepository<Applicant>();

            var job = await _jobRepo.GetFirstOrDefaultAsync(predicate: x => x.Id == jobId);
            var applicants = await _applicantRepo.GetAllAsync(predicate: x => x.JobId == jobId, include: x => x.Include(y => y.User));

            var jobApplicantDTO = new JobApplicantDto()
            {
                Job = _mapper.Map<JobDto>(job),
                Applicants = applicants.Select(x => _mapper.Map<ApplicantDto>(x)).ToList()
            };

            return OperationResult<JobApplicantDto>.Success(jobApplicantDTO);
        }

        public async Task<OperationResult<IEnumerable<ApplicantDto>>> GetApplicantsByUserIdAsync()
        {
            var _applicantRepo = _uow.GetRepository<Applicant>();

            var jwt = _jwt.GetJwtUserResource();

            var applicants = await _applicantRepo.GetAllAsync(predicate: x => x.UserId == jwt.Id,
                include: x => x.Include(y => y.User).Include(y => y.Job));

            return OperationResult<IEnumerable<ApplicantDto>>.Success(applicants.Select(x => _mapper.Map<ApplicantDto>(x)));
        }

        public async Task<OperationResult<JobDto>> GetJobByIdAsync(int id)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var job = await _jobRepo.GetFirstOrDefaultAsync(predicate: x => x.Id == id);

            if (job != null)
            {
                return OperationResult<JobDto>.Success(_mapper.Map<JobDto>(job));
            }

            return OperationResult<JobDto>.Failed(new[] { $"Could not find job with id: {id}" });
        }

        public OperationResult MarkJobAsFilled(int jobId)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var job = _jobRepo.GetFirstOrDefault(predicate: x => x.Id == jobId, disableTracking: false);

            if (job != null)
            {
                job.IsFilled = true;

                try
                {
                    _jobRepo.Update(job);
                    _uow.SaveChanges();

                    return OperationResult.Success;
                }
                catch
                {
                    return OperationResult.Failed(new[] { $"An error occurred while marking the job to be filled" });
                }
            }

            return OperationResult<JobDto>.Failed(new[] { $"Unable to mark job to be filled with id: {jobId}" });
        }

        public async Task<OperationResult> UpdateJobAsync(JobUpdateRequestModel model)
        {
            var _jobRepo = _uow.GetRepository<Job>();

            var jwt = _jwt.GetJwtUserResource();

            var jobEntity = await _jobRepo.GetFirstOrDefaultAsync(predicate: x => x.Id == model.Id, disableTracking: false);

            if (jobEntity != null)
            {
                var user = await _userManager.FindByIdAsync(jwt.Id);

                if (user != null)
                {
                    _mapper.Map<JobUpdateRequestModel, Job>(model, jobEntity);

                    try
                    {
                        _jobRepo.Update(jobEntity);
                        await _uow.SaveChangesAsync();

                        return OperationResult.Success;
                    }
                    catch
                    {
                        return OperationResult.Failed(new[] { $"An error occurred while updating the job" });
                    }
                }
                else
                {
                    return OperationResult.Failed(new[] { $"User not found" });
                }
            }

            return OperationResult.Failed(new[] { $"Job does not exists. Invalid job id: {model.Id}" });
        }
    }
}
