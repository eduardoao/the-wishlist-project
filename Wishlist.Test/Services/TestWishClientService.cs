using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Services;

namespace Wishlist.Test.Services
{
    [TestClass]
    public class TestWishClientService
    {

        private ServiceWishClient serviceWishClient;
        private Mock<IRepositoryWishClient> repositoryWhisClient;
        private Mock<IRepositoryProduct> repositoryProduct;

        public TestWishClientService()
        {
            repositoryWhisClient = new Mock<IRepositoryWishClient>();
            repositoryProduct = new Mock<IRepositoryProduct>();
            serviceWishClient = new ServiceWishClient(repositoryWhisClient.Object, repositoryProduct.Object);
        }

        [TestMethod]
        public void WishList_Client_Have_List()
        {

            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            var title = new Title("Notebook");
            var picture = new Picture("/teste/teste.jpeg");
            var product = Product.ProductBuilder(title, picture, 10000, "Dell");

            

            var wishlist = WishClient.WishClientBuilder(client, product);

            repositoryWhisClient.Setup(x => x.GetByClientEmail(client.Email.ToString())).Returns((WishClient)null);
            repositoryProduct.Setup(x => x.GetByProductTitle(product.Title.ToString())).Returns(product);

            serviceWishClient.Add(wishlist);
            var list = serviceWishClient.GetErrors();

            Assert.AreEqual(0, list.Count);

        }



        [TestMethod]
        public void WishList_Creat_Success()
        {          

            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            var title = new Title("Notebook");
            var picture = new Picture("/teste/teste.jpeg");
            var product = Product.ProductBuilder(title, picture, 10000, "Dell");

            var wishlist = WishClient.WishClientBuilder(client, product);

            repositoryWhisClient.Setup(x => x.GetByClientEmail(client.Email.ToString())).Returns((WishClient)null);
            repositoryProduct.Setup(x => x.GetByProductTitle(product.Title.ToString())).Returns(product);
           
            serviceWishClient.Add(wishlist);
            var list = serviceWishClient.GetErrors();

            Assert.AreEqual(0, list.Count);

        }
    }
}
