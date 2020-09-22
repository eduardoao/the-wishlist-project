using System;
using Wishlist.Core.Models;

namespace Wishlist.Core.Interfaces.Repositorys
{
    public interface IRepositoryWishClient : IRepositoryBase<WishClient>
    {
        WishClient GetByClientEmail(string id);
        bool ProductExistInWish(Guid id, string title);
        void AddItem(WishClient obj);
     
    }
}
