using Microsoft.EntityFrameworkCore;

namespace kit19Task.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions): base(contextOptions)
        {

        }
    }
}
