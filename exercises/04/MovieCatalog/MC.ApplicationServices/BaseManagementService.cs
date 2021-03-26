using MC.Data.Contexts;

namespace MC.ApplicationServices
{
    public class BaseManagementService
    {
        #region
        protected readonly MovieCatalogDbContext _context = new MovieCatalogDbContext();
        #endregion
    }
}
