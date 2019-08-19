using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.GenericRepository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
    }
}
