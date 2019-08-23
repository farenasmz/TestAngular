using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Helpers;
using Infraestructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace TestAngular.Controllers
{
	[Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository Repository;
		private readonly IUserRepository UserRepository;
		private readonly IBookProduct ProductRepository;

		public ProductsController(IProductRepository repository, IUserRepository UserRepository, IBookProduct ProductRepository)
		{
			Repository = repository;
			this.UserRepository = UserRepository;
			this.ProductRepository = ProductRepository;
		}

		// GET: api/Products
		[HttpGet]
		public IActionResult GetProducts()
		{
            return this.Ok(this.Repository.GetAll());
		}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
		{
			Product product;

			try
			{
				product = await this.Repository.GetByIdAsync(id);

				if (product == null)
				{
					return NotFound();
				}
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return product;
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

				product.AvailableQuantity = product.Quantity;
				await this.Repository.UpdateAsync(product);
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
			Product product;
			User user;
			BookProduct book;

			try
			{
				product = await this.Repository.GetByIdAsync(bookProduct.ProductId);
				user = await this.UserRepository.GetByEmail(bookProduct.Email);

				if (product == null || user == null)
				{
					return NotFound();
				}

				if (product.AvailableQuantity == 0)
				{
					return BadRequest("There is no more quantity available");
				}

				book = this.ProductRepository.GetBookByPersonAndProduct(user.Id, product.Id);

				if (book == null)
				{
					book = new BookProduct();
					book.ProductId = product.Id;
					book.UserID = user.Id;
					book.Quantity = 1;
					product.AvailableQuantity -= 1;
					await this.ProductRepository.CreateAsync(book);
				}

				if (bookProduct.Value < 0)
				{
					book.Quantity -= 1;
					product.AvailableQuantity += 1;
				}
				else
				{
					book.Quantity += 1;
					product.AvailableQuantity -= 1;
				}

				if (product.AvailableQuantity > product.Quantity)
				{
					return BadRequest("There is no more quantity available");
				}

				await this.ProductRepository.UpdateAsync(book);
				await this.Repository.UpdateAsync(product);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}


		[HttpPost]
		[Route("ResetProduct")]
		public async Task<IActionResult> ResetProductAsync(int productId, [FromBody] User user)
		{
			Product product;
			User tmpUser;

			try
			{
				user.Password = Security.sha256_hash(user.Password);
				tmpUser = await UserRepository.ValidateEmailAndPassword(user.Email, user.Password);

				if (tmpUser != null)
				{
					if (tmpUser.isActive)
					{
						product = await this.Repository.GetByIdAsync(productId);

						using (TransactionScope transaction = new TransactionScope())
						{
							this.ProductRepository.ResetProduct(product.Id);
							this.Repository.ResetProductQuantity(product.Id, product.Quantity);
							transaction.Complete();
							return Ok();
						}
					}
					else
					{
						return BadRequest("Blocked user.");
					}
				}

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
				product.IsActive = true;
				product.AvailableQuantity = product.Quantity;
				await this.Repository.CreateAsync(product);
				return CreatedAtAction("GetProduct", new { id = product.Id }, product);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Product>> DeleteProduct(int id)
		{
			Product product;
			try
			{
				product = await this.Repository.GetByIdAsync(id);

				if (product == null)
				{
					return NotFound();
				}

				product.IsActive = false;
				await this.Repository.UpdateAsync(product);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
