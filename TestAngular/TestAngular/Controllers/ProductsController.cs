﻿using Infraestructure.GenericRepository;
using Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace TestAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository Repository;
        private readonly IUserRepository UserRepository;

        public ProductsController(IProductRepository repository, IUserRepository UserRepository)
        {
            Repository = repository;
            this.UserRepository = UserRepository;
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

                await this.Repository.UpdateAsync(product);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("Create")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookProduct(int productID, string email)
        {
            Product product;
            User user;

            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    product = await this.Repository.GetByIdAsync(productID);
                    user = await this.UserRepository.GetByEmail(email);

                    if (product == null || user == null)
                    {
                        return NotFound();
                    }

                    if (product.Quantity == 0)
                    {
                        return BadRequest();
                    }

                    product.Quantity -= 1;
                    await this.Repository.UpdateAsync(product);
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            try
            {
                product.IsActive = true;
                await this.Repository.CreateAsync(product);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception)
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
