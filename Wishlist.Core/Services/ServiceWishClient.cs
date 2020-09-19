using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;

namespace Wishlist.Core.Services
{
    public class ServiceWishClient : BaseService<WishClient>, IWishClientService
    {
        public readonly IRepositoryWishClient _repositoryWishClient;

        public ServiceWishClient(IRepositoryWishClient repositoryWishClient)
            : base(repositoryWishClient)
        {
            _repositoryWishClient = repositoryWishClient;
        }

    }
}
