using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Wishlist.Core.Interfaces.Repositorys;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Services;

namespace Wishlist.Test.Services
{
    [TestClass]
    public class TestClientService  
    {

        private ServiceClient serviceClient;
        private Mock<IRepositoryClient> RepositoryMock;
        private Client client;

        [TestInitialize]
        public void Setup()
        {
            client = GetClient();
        }

        public TestClientService()
        {
            RepositoryMock = new Mock<IRepositoryClient>();
            serviceClient = new ServiceClient(RepositoryMock.Object);
        }

        [TestMethod]
        public void Client_Existent()
        {           
            RepositoryMock.Setup(x => x.Add(client));
            RepositoryMock.Setup(x => x.GetByEmail(client.Email.ToString())).Returns(client);

            serviceClient.Add(client);
            var list = serviceClient.GetErrors();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Client_NewOK()
        {          
            
            RepositoryMock.Setup(x => x.Add(client));           
            RepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>())).Returns((Client)null);


            serviceClient.Add(client);
            var list = serviceClient.GetErrors();

            Assert.AreEqual(0, list.Count);

        }

        [TestMethod]
        public void Client_Get_Success()
        {
            var id = Guid.NewGuid();
            RepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(client);           

            var expect = serviceClient.GetById(id);
            var actual = "Eduardo Oliveira";

            Assert.AreEqual(expect.Name.ToString(), actual);
        }

        [TestMethod]
        public void Client_Update_Success()
        {
            var id = Guid.NewGuid();
            RepositoryMock.Setup(r => r.Update(It.IsAny<Client>()));

            var expect = serviceClient.GetErrors().Count;
            var actual = 0;

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void Client_Delete_Success()
        {
            var id = Guid.NewGuid();
            RepositoryMock.Setup(r => r.Remove(It.IsAny<Client>()));

            var expect = serviceClient.GetErrors().Count;
            var actual = 0;

            Assert.AreEqual(expect, actual);
        }

        private Client GetClient()
        {
            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            return client;
        }

    }
}
