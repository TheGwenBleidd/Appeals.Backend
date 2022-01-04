using Appeals.Domain;
using Appeals.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Test.Common
{
    public class AppealsContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid AppealIdForDelete = Guid.NewGuid();
        public static Guid AppealIdForUpdate = Guid.NewGuid();

        public static AppealsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppealsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AppealsDbContext(options);
            context.Database.EnsureCreated();
            context.Appeals.AddRange(
                new Appeal
                {
                    CreatedDate = DateTime.Today,
                    Description = "Details1",
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Appeal
                {
                    CreatedDate = DateTime.Today,
                    Description = "Details2",
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Appeal
                {
                    CreatedDate = DateTime.Today,
                    Description = "Details3",
                    Id = AppealIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Appeal
                {
                    CreatedDate = DateTime.Today,
                    Description = "Details4",
                    Id = AppealIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(AppealsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
