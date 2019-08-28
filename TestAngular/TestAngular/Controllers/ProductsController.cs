using Core.Products;
using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TestAngular.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
    public class ProductsController : ControllerBase
	{
		private readonly ProductsBussiness ProductsBusiness;

		public ProductsController(IProductRepository repository, IUserRepository UserRepository, IBookProduct ProductRepository)
		{
			ProductsBusiness = new ProductsBussiness(repository, UserRepository, ProductRepository);
		}

		// GET: api/Products
		[HttpGet]
		public IActionResult GetProducts()
		{
			return Ok(ProductsBusiness.GetAllProducts());
		}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
		{
			try
			{
				return await ProductsBusiness.GetProductById(id);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		// PUT: api/Products/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct([FromBody] Product product)
		{
			try
			{
				if (product == null)
				{
					return NotFound();
				}

				await ProductsBusiness.PutProduct(product);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("BookProduct")]
		public async Task<IActionResult> BookProduct([FromBody] BookProductDto bookProduct)
		{
			try
			{
				await ProductsBusiness.BookProduct(bookProduct);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}


		[HttpPost]
		[Route("ResetProduct")]
		public async Task<IActionResult> ResetProductAsync(int productId, [FromBody] User user)
		{
			try
			{
				await ProductsBusiness.ResetProducts(productId, user);
				return BadRequest("Wrong Password");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// POST: api/Products
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
		{
			try
			{
				product = await ProductsBusiness.CreateProduct(product);
				return CreatedAtAction("GetProduct", new { id = product.Id }, product);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			try
			{
				await ProductsBusiness.DisableProduct(id);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
