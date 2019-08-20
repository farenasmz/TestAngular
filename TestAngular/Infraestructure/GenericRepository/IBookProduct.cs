using Infraestructure.Models;

namespace Infraestructure.GenericRepository
{
	public interface IBookProduct : IGenericRepository<BookProduct>
	{
		BookProduct GetBookByPersonAndProduct(int userId, int ProductId);
		void ResetProduct(int productId);
	}
}
