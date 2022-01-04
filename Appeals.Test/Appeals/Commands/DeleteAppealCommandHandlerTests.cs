using Appeals.Application.Appeals.Commands.CreateAppeal;
using Appeals.Application.Appeals.Commands.DeleteAppeal;
using Appeals.Application.Common.Exceptions;
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
    public class DeleteAppealCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteAppealCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteAppealCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteAppealCommand
            {
                Id = AppealsContextFactory.AppealIdForDelete,
                UserId = AppealsContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(await Context.Appeals.SingleOrDefaultAsync(appeal =>
                appeal.Id == AppealsContextFactory.AppealIdForDelete));
        }

        [Fact]
        public async Task DeleteAppealCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteAppealCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteAppealCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = AppealsContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteAppealCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteAppealCommandHandler(Context);
            var createHandler = new CreateAppealCommandHandler(Context);
            var appealId = await createHandler.Handle(
                new CreateAppealCommand
                {
                    Title = "AppealTitle",
                    UserId = AppealsContextFactory.UserAId
                }, CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteAppealCommand
                    {
                        Id = appealId,
                        UserId = AppealsContextFactory.UserBId
                    }, CancellationToken.None));
        }
    }
}
