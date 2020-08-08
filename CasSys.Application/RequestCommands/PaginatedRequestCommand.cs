using CasSys.Application.Attributes;

namespace CasSys.Application.RequestCommands
{
    public class PaginatedRequestCommand
    {
        public PaginatedRequestCommand() { }

        public PaginatedRequestCommand(int page, int take)
        {
            Page = page;
            Take = take;
        }

        [Minimum(1)]
        public int Page { get; set; }

        [Minimum(1)]
        [Maximum(10)]
        public int Take { get; set; }
    }
}