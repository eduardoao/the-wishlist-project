using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Services;

namespace Wishlist.Test.Services
{
    [TestClass]
    public class TestProductService
    {

        private ServiceProduct serviceProduct;
        private Mock<IRepositoryProduct> RepositoryMock;
        private Product product;

        [TestInitialize]
        public void Setup()
        {
            product = GetProduct();
        }

        public TestProductService()
        {
            RepositoryMock = new Mock<IRepositoryProduct>();
            serviceProduct = new ServiceProduct(RepositoryMock.Object);
        }

        [TestMethod]
        public void Product_Existent()
        {           
            RepositoryMock.Setup(x => x.Add(product));
            RepositoryMock.Setup(x => x.GetByProductTitle(It.IsAny<string>())).Returns(product);

            serviceProduct.Add(product);
            var list = serviceProduct.GetErrors();
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Product_Add_Success()
        {
            RepositoryMock.Setup(x => x.Add(product));
            RepositoryMock.Setup(x => x.GetByProductTitle(It.IsAny<string>())).Returns((Product)null);

            serviceProduct.Add(product);
            var list = serviceProduct.GetErrors();
            Assert.AreEqual(0, list.Count);
        }            

        [TestMethod]
        public void Product_Get_List_Success()
        {            
            RepositoryMock.Setup(r => r.GetAll()).Returns(GetListProduct());

            var expect = serviceProduct.GetAll();
            var actual = 2;

            Assert.AreEqual(expect.Count(), actual);
        }     


        private Product GetProduct()
        {
            var title = new Title("Celular Motorola G8");
            var picture = new Picture("https://a-static.mlcdn.com.br/618x463/smartphone-motorola-moto-g8-power-lite-64gb-azul-4g-octa-core-4gb-ram-65-cam-tripla-selfie-8mp/magazineluiza/155578400/5dd5f43c40e4ebafb0adabe41799b4df.jpg");
            var product = Product.ProductBuilder(title, picture, 1249, "motorola");
            return product;  
        }


        private List<Product> GetListProduct()
        {
            var listProduct = new List<Product>();

            var titleCelular = new Title("Celular Motorola G8");
            var pictureCelular = new Picture("https://a-static.mlcdn.com.br/618x463/smartphone-motorola-moto-g8-power-lite-64gb-azul-4g-octa-core-4gb-ram-65-cam-tripla-selfie-8mp/magazineluiza/155578400/5dd5f43c40e4ebafb0adabe41799b4df.jpg");
            var productCelular = Product.ProductBuilder(titleCelular, pictureCelular, 1249, "motorola");

            var titleKeyBoard = new Title("Teclado Mecânico Corsair");
            var pictureKeyBoard = new Picture("https://a-static.mlcdn.com.br/618x463/conjunto-de-teclas-para-teclado-mecanico-corsair-ch-9000235-ww-keycaps-preto/estrela10/218854/2b46fbe804b4baf42e6680f9a5ef2b01.jpg");
            var productKeyBoard = Product.ProductBuilder(titleKeyBoard, pictureKeyBoard, 1249, "motorola");

            listProduct.Add(productCelular);
            listProduct.Add(productKeyBoard);
            return listProduct;
        }

    }
}

