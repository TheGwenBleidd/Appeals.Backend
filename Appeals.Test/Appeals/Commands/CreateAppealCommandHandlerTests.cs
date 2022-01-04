using Appeals.Application.Appeals.Commands.CreateAppeal;
using Appeals.Test.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Appeals.Test.Appeals.Commands
{
    public class CreateAppealCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateAppealCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateAppealCommandHandler(Context);
            var appealName = "appeal name";
            var appealDescription= "appeal description";

            // Act
            var appealId = await handler.Handle(
                new CreateAppealCommand
                {
                    Title = appealName,
                    Description = appealDescription,
                    UserId = AppealsContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Appeals.SingleOrDefaultAsync(appeal =>
                    appeal.Id == appealId && appeal.Title == appealName &&
                    appeal.Description == appealDescription));
        }
    }
}
