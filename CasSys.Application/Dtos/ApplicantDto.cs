using System.Collections.Generic;

namespace CasSys.Application.Dtos
{
    public class ApplicantDto
    {
        public UserDto User { get; set; }
        public JobDto Job { get; set; }
    }

    public class JobApplicantDto
    {
        public JobDto Job { get; set; }
        public IList<ApplicantDto> Applicants { get; set; }
    }
}
