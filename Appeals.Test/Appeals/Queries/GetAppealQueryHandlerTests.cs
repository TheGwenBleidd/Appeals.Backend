using Appeals.Application.Appeals.Queries.GetAppeal;
using Appeals.Persistance;
using Appeals.Test.Common;
using AutoMapper;
using Shouldly;
using Xunit;

namespace Appeals.Test.Appeals.Queries
{
    [Collection("QueryCollection")]
    public class GetAppealQueryHandlerTests
    {
        private readonly AppealsDbContext Context;
        private readonly IMapper Mapper;

        public GetAppealQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAppealQueryHandler_Success()
        {
            // Arrange
            var handler = new GetAppealQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetAppealQuery
                {
                    UserId = AppealsContextFactory.UserBId,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<AppealVm>();
            result.Title.ShouldBe("Title2");
            result.CreatedDate.ShouldBe(DateTime.Today);
        }
    }
}
