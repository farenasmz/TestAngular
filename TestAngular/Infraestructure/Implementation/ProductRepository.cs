using Infraestructure.GenericRepository;
using Infraestructure.Models;

namespace Infraestructure.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
