using DB.dbContext;

namespace Services.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoesStoreDbContext _ctx;

        public UnitOfWork(ShoesStoreDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> SaveChangesAsync()
            => await _ctx.SaveChangesAsync();

    }
}
