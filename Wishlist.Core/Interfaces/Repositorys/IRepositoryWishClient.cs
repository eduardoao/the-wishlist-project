using System;
using Wishlist.Core.Models;

namespace Wishlist.Core.Interfaces.Repositorys
{
    public interface IRepositoryWishClient : IRepositoryBase<WishClient>
    {
        WishClient GetByClientEmail(string id);

        void AddItem(WishClient obj);
    }
}
