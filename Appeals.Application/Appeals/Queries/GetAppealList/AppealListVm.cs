using Appeals.Application.Common.Mappings;
using Appeals.Domain;

namespace Appeals.Application.Appeals.Queries.GetAppealList
{
    public class AppealListVm : IMapWith<Appeal>
    {
        public IList<AppealLookUpDto> Appeals { get; set; } = null!;
    }
}
