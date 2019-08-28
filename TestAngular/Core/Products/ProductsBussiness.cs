using Infraestructure.Dto;
using Infraestructure.GenericRepository;
using Infraestructure.Helpers;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Products
{
	public class ProductsBussiness
	{
		private readonly IProductRepository Repository;
		private readonly IUserRepository UserRepository;
		private readonly IBookProduct ProductRepository;

		public ProductsBussiness(IProductRepository repository, IUserRepository UserRepository, IBookProduct ProductRepository)
		{
			Repository = repository;
			this.UserRepository = UserRepository;
			this.ProductRepository = ProductRepository;
		}

		public List<Product> GetAllProducts()
		{
			return Repository.GetAll().ToList();
		}

		public async Task<Product> GetProductById(int id)
		{
			Product product = await Repository.GetByIdAsync(id);

			if (product == null)
			{
				throw new KeyNotFoundException();
			}

			return product;
		}

		public async Task PutProduct(Product product)
		{
			product.AvailableQuantity = product.Quantity;
			await Repository.UpdateAsync(product);
		}

		public async Task BookProduct(BookProductDto bookProduct)
		{
			Product product;
			User user;
			BookProduct book;

			product = await Repository.GetByIdAsync(bookProduct.ProductId);
			user = await UserRepository.GetByEmail(bookProduct.Email);

			if (product == null || user == null)
			{
				throw new KeyNotFoundException();
			}

			if (product.AvailableQuantity == 0)
			{
				throw new InvalidOperationException("There is no more quantity available");
			}

			book = ProductRepository.GetBookByPersonAndProduct(user.Id, product.Id);

			if (book == null)
			{
				book = new BookProduct
				{
					ProductId = product.Id,
					UserID = user.Id,
					Quantity = 1
				};
				product.AvailableQuantity -= 1;
				await ProductRepository.CreateAsync(book);
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
				throw new InvalidOperationException("There is no more quantity available");
			}

			await ProductRepository.UpdateAsync(book);
			await Repository.UpdateAsync(product);
		}

		public async Task ResetProducts(int productId, User user)
		{
			Product product;
			User tmpUser;
			user.Password = Security.Sha256_hash(user.Password);
			tmpUser = await UserRepository.ValidateEmailAndPassword(user.Email, user.Password);

			if (tmpUser != null)
			{
				if (tmpUser.isActive)
				{
					product = await Repository.GetByIdAsync(productId);

					using (TransactionScope transaction = new TransactionScope())
					{
						ProductRepository.ResetProduct(product.Id);
						Repository.ResetProductQuantity(product.Id, product.Quantity);
						transaction.Complete();
					}
				}
				else
				{
					throw new InvalidOperationException("Not Valid Login"); 
				}
			}
			else
			{
				throw new KeyNotFoundException();
			}
		}

		public async Task<Product> CreateProduct(Product product)
		{
			product.IsActive = true;
			product.AvailableQuantity = product.Quantity;
			await Repository.CreateAsync(product);
			return product;
		}

		public async Task DisableProduct(int id)
		{
			Product product = await Repository.GetByIdAsync(id);

			if (product == null)
			{
				throw new KeyNotFoundException();
			}

			product.IsActive = false;
			await Repository.UpdateAsync(product);
		}
	}
}