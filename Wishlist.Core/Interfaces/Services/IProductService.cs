using System.Collections.Generic;
using Wishlist.Core.Models;

namespace Wishlist.Core.Interfaces.Services
{
    public interface IProductService : IServiceBase<Product>
    {
        public IList<Product> GetProductsPaged(int limit);
    }
}
