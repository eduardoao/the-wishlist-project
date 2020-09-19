using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;

namespace Wishlist.Core.Services
{
    public class ServiceProduct : BaseService<Product>, IProductService
    {
        public readonly IRepositoryProduct _repositoryProduct;

        public ServiceProduct(IRepositoryProduct RepositoryProduct)
            : base(RepositoryProduct)
        {
            _repositoryProduct = RepositoryProduct;
        }

    }
}
