using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Interfaces.Services;
using Wishlist.Core.Models;

namespace Wishlist.Core.Services
{
    public class ServiceClient : BaseService<Client>, IClientService
    {
        public readonly IRepositoryClient _repositoryClient;

        public ServiceClient(IRepositoryClient RepositoryCliente)
            : base(RepositoryCliente)
        {
            _repositoryClient = RepositoryCliente;
        }

    }
}
