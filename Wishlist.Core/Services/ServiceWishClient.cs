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
        public readonly IRepositoryClient _repositoryClient;

        public ServiceWishClient(IRepositoryWishClient repositoryWishClient, IRepositoryProduct repositoryProduct, IRepositoryClient repositoryClient)
            : base(repositoryWishClient)
        {
            _repositoryWishClient = repositoryWishClient;
            _repositoryProduct = repositoryProduct;
            _repositoryClient = repositoryClient;
        }
        public override void Add(WishClient obj)
        {
            var clientexist = _repositoryClient.GetById(obj.Client.Id);
            if (clientexist == null)
            {
                Errors.Add(new Error("003", "Cliente não existe na base de dados!"));
                return;
            }

            var productexist = _repositoryProduct.GetByProductTitle(obj.Product.Title.ToString());

            if (productexist == null)
            {
                Errors.Add(new Error("003", "Produto não existe na base de dados!"));
                return;
            }

            var wishclientexist = _repositoryWishClient.GetByClientEmail(obj.Client.Email.ToString());

            if (wishclientexist != null)
            {                 

                var productexistinwichlist = _repositoryWishClient.ProductExistInWish(wishclientexist.Id, obj.Product.Title.ToString());

                if (productexistinwichlist)
                {
                    Errors.Add(new Error("003", "O produto selecionado já consta na lista de desejos!"));
                    return;
                }
                _repositoryWishClient.AddItem(obj);
            }

            else
            {
                var wishlist = WishClient.WishClientBuilder(clientexist, productexist);
                _repositoryWishClient.Add(wishlist);
                //todo  Retornar a lista para adicionar no item
                _repositoryWishClient.AddItem(obj);

            }

            Errors.Add(new Error("003", "Cliente nao existe na base da dados!"));
            return;

            //base.Add(obj);
        }

    }
}
