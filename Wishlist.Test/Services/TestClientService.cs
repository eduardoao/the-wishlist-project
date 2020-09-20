using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        public TestClientService()
        {
            RepositoryMock = new Mock<IRepositoryClient>();
            serviceClient = new ServiceClient(RepositoryMock.Object);
        }

        [TestMethod]
        public void Client_Existent()
        {

            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            RepositoryMock.Setup(x => x.Add(client));
            RepositoryMock.Setup(x => x.GetByEmail(client.Email.ToString())).Returns(client);

            serviceClient.Add(client);
            var list = serviceClient.GetErrors();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Client_NewOK()
        {

            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira"), new Email("email@email.com.br"));
            
            RepositoryMock.Setup(x => x.Add(client));           
            RepositoryMock.Setup(r => r.GetByEmail(It.IsAny<string>())).Returns((Client)null);


            serviceClient.Add(client);
            var list = serviceClient.GetErrors();

            Assert.AreEqual(0, list.Count);

        }
    }
}
