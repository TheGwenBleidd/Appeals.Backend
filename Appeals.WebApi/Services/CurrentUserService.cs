using Appeals.Application.Interfaces;

namespace Appeals.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor) =>
            _contextAccessor = contextAccessor;
        
        public Guid UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
