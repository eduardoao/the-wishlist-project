using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Net.WebSockets;
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
        private Mock<IRepositoryClient> repositoryClient;

        public TestWishClientService()
        {
            repositoryWhisClient = new Mock<IRepositoryWishClient>();
            repositoryProduct = new Mock<IRepositoryProduct>();
            repositoryClient = new Mock<IRepositoryClient>();
            serviceWishClient = new ServiceWishClient(repositoryWhisClient.Object, repositoryProduct.Object, repositoryClient.Object);
        }

        [TestMethod]
        public void WishList_Client_Created_Success()
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


        //[TestMethod]
        public void WishList_Client_Have_One_List()
        {
            GetWishClient(out Client client, out Product product, out WishClient wishlist);

            repositoryWhisClient.Setup(x => x.GetByClientEmail(client.Email.ToString())).Returns((WishClient)wishlist);
            repositoryProduct.Setup(x => x.GetByProductTitle(product.Title.ToString())).Returns(product);

            serviceWishClient.Add(wishlist);
            var list = serviceWishClient.GetErrors().ToList();

            var expect = "Cliente já possui uma lista de desejo na base de dados!";
            var actual = list[0].Message;

            Assert.AreEqual(expect, actual);

        }


        [TestMethod]
        public void WishList_Client_Have_One_Product_List()
        {
            GetWishClient(out Client client, out Product product, out WishClient wishlist);

            repositoryWhisClient.Setup(x => x.GetByClientEmail(client.Email.ToString())).Returns((WishClient)wishlist);
            repositoryProduct.Setup(x => x.GetByProductTitle(product.Title.ToString())).Returns(product);
            repositoryWhisClient.Setup(x => x.ProductExistInWish(It.IsAny<Guid>(), It.IsAny<string>())).Returns(true);            

            serviceWishClient.Add(wishlist);
            var list = serviceWishClient.GetErrors().ToList();

            var expect = "O produto selecionado já consta na lista de desejos!";
            var actual = list[0].Message;

            Assert.AreEqual(expect, actual);

        }
    

        private static void GetWishClient(out Client client, out Product product, out WishClient wishlist)
        {
            client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            var title = new Title("Notebook");
            var picture = new Picture("/teste/teste.jpeg");
            product = Product.ProductBuilder(title, picture, 10000, "Dell");
            wishlist = WishClient.WishClientBuilder(client, product);            
        }              
    }
}
