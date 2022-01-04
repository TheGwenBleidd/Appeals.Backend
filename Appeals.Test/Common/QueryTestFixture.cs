using Appeals.Application.Common.Mappings;
using Appeals.Persistance;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Appeals.Test.Common
{
    public class QueryTestFixture : IDisposable
    {
        public AppealsDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = AppealsContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(AppealsDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            AppealsContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
