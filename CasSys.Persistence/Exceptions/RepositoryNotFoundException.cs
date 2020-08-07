using System;

namespace CasSys.Persistence.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException(string repositoryName, string message)
            : base(message)
        {
            if (string.IsNullOrWhiteSpace(repositoryName)) 
                throw new ArgumentException($"{nameof(repositoryName)} cannot be null or empty.", nameof(repositoryName));

            this.RepositoryName = repositoryName;
        }

        public string RepositoryName { get; private set; }
    }
}