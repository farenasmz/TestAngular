using Infraestructure.Models;

namespace Infraestructure.GenericRepository
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		void ResetProductQuantity(int productId, int quantity);
	}
}
