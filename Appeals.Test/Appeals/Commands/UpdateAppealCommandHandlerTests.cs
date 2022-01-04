using Appeals.Application.Appeals.Commands.UpdateAppeal;
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
    public class UpdateAppealCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateAppealCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateAppealCommandHandler(Context);
            var updatedTitle = "new title";

            // Act
            await handler.Handle(new UpdateAppealCommand
            {
                Id = AppealsContextFactory.AppealIdForUpdate,
                UserId = AppealsContextFactory.UserBId,
                Title = updatedTitle
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Appeals.SingleOrDefaultAsync(appeal =>
                appeal.Id == AppealsContextFactory.AppealIdForUpdate &&
                appeal.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateAppealCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateAppealCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateAppealCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = AppealsContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateAppealCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateAppealCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new UpdateAppealCommand
                    {
                        Id = AppealsContextFactory.AppealIdForUpdate,
                        UserId = AppealsContextFactory.UserAId
                    },
                    CancellationToken.None);
            });
        }
    }
}
