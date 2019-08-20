using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

namespace Infraestructure.Implementation
{
	public class BookRepository : GenericRepository<BookProduct>, IBookProduct
	{
		private readonly DataContext context;
		public BookRepository(DataContext context) : base(context)
		{
			this.context = context;
		}

		public BookProduct GetBookByPersonAndProduct(int userId, int ProductId)
		{
			return context.BookProducts.Where(book => book.UserID == userId && book.ProductId == ProductId).FirstOrDefault();
		}

		public void ResetProduct(int productId)
		{
			string sql = " UPDATE BookProducts SET quantity = 0 WHERE PRODUCTID = @ProductID";
			SqlParameter pProductId = new SqlParameter("@ProductID", productId);
			context.Database.ExecuteSqlCommand(sql, pProductId);
		}
	}
}
