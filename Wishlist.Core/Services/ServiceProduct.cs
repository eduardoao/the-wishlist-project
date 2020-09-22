using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Services
{
    public class ServiceProduct : BaseService<Product>
    {
        public readonly IRepositoryProduct _repositoryProduct;

        public ServiceProduct(IRepositoryProduct RepositoryProduct)
            : base(RepositoryProduct)
        {
            _repositoryProduct = RepositoryProduct;
        }

        public override void Add(Product obj)
        {
            var productexist = _repositoryProduct.GetByProductTitle(obj.Title.ToString());
            if (productexist != null)
                Errors.Add(new Error("002", "Produto já existente na base de dados!"));
            base.Add(obj);
        }



    }
}
