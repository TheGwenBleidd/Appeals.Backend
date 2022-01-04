using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Persistance
{
    public class DbInitializer
    {
        public static void Initialize(AppealsDbContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
