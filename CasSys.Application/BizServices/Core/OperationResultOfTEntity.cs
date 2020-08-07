namespace CasSys.Application.BizServices.Core
{
    public class OperationResult<TEntity> : OperationResult
    {
        public new static OperationResult<TEntity> Success(TEntity entity) => new OperationResult<TEntity> { Succeeded = true, Entity = entity };

        public new static OperationResult<TEntity> Failed(params string[] errors)
        {
            var result = new OperationResult<TEntity> { Succeeded = false };

            if (errors != null)
            {
                result._errors.AddRange(errors);
            }

            return result;
        }

        public TEntity Entity { get; set; }
    }
}