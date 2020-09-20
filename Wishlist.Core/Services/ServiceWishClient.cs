using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Services
{
    public class ServiceWishClient : BaseService<WishClient>, IWishClientService
    {
        public readonly IRepositoryWishClient _repositoryWishClient;
        public readonly IRepositoryProduct _repositoryProduct;

        public ServiceWishClient(IRepositoryWishClient repositoryWishClient, IRepositoryProduct repositoryProduct)
            : base(repositoryWishClient)
        {
            _repositoryWishClient = repositoryWishClient;
            _repositoryProduct = repositoryProduct;
        }


        public override void Add(WishClient obj)
        {
            var wishclientexist = _repositoryWishClient.GetByClientEmail(obj.Client.Email.ToString());

            if (wishclientexist != null)
                Errors.Add(new Error("003", "Cliente já possui uma lista de desejo na base de dados!"));

            var productexist = _repositoryProduct.GetByProductTitle(obj.Product.Title.ToString());

            if (productexist == null)
                Errors.Add(new Error("003", "Produto não existe na base de dados!"));

            base.Add(obj);
        }


    }
}
