using MediatR;

namespace CMS.Backend.Shared.Types
{
    public interface IPagedQuery : IRequest
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}