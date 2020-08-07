using System.Collections.Generic;

namespace CasSys.Application.BizServices.Core
{
    public class OperationResult
    {
        protected List<string> _errors = new List<string>();

        public bool Succeeded { get; protected set; }

        public IEnumerable<string> Errors => _errors;

        public static OperationResult Success { get; } = new OperationResult { Succeeded = true };

        public static OperationResult Failed(params string[] errors)
        {
            var result = new OperationResult { Succeeded = false };

            if (errors != null)
            {
                result._errors.AddRange(errors);
            }

            return result;
        }
    }
}