using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Infraestructure.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

		public void ResetProductQuantity(int productId, int quantity)
		{
			string sql = "UPDATE Products SET quantity = @Quantity WHERE PRODUCTID = @ProductID; ";
			SqlParameter pQuantity = new SqlParameter("@Quantity", quantity);
			SqlParameter pProductId = new SqlParameter("@ProductID", productId);
			context.Database.ExecuteSqlCommand(sql, pQuantity, pProductId);
		}
	}
}
