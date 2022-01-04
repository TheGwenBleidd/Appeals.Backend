using Appeals.Application.Appeals.Queries.GetAppealList;
using Appeals.Persistance;
using Appeals.Test.Common;
using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Appeals.Test.Appeals.Queries
{
    [Collection("QueryCollection")]
    public class GetAppealListQueryHandlerTests
    {
        private readonly AppealsDbContext Context;
        private readonly IMapper Mapper;

        public GetAppealListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAppealListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetAppealListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetAppealListQuery
                {
                    UserId = AppealsContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<AppealListVm>();
            result.Appeals.Count.ShouldBe(2);
        }
    }
}
