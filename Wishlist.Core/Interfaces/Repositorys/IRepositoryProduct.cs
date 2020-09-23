using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Models;

namespace Wishlist.Core.Interfaces.Repositorys
{
    public interface IRepositoryProduct : IRepositoryBase<Product>
    {
        Product GetByProductTitle(string name);

        IList<Product> GetProductsPaged(int limit, int offset);

    }
}
