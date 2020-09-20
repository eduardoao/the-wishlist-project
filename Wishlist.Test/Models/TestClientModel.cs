using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Wishlist.Core.Models;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Test.Model
{
    [TestClass]
    public class TestClientModel
    {
       [TestMethod]
        public void Client_Created_Success()
        {
            var client = Client.ClientBuilder(new Name("Eduardo", "Oliveira") , new Email("email@email.com.br"));
            var target = client.IsValid();
            var expect = true;
            Assert.AreEqual(expect, target);         
        }

        [TestMethod]
        public void Client_ShouldNameBeValid()
        {
            var client = Client.ClientBuilder(new Name("", "Oliveira"), new Email("email@email.com.br"));
            var target = client.GetValidationResults()[0].ErrorMessage;
            var expect = "O primero nome não pode estar em branco";
            Assert.AreEqual(expect, target);
        }

        [TestMethod]
        public void Client_ShouldLastNameBeValid()
        {
            var client = Client.ClientBuilder(new Name("Eduardo", ""), new Email("email@email.com.br"));
            var target = client.GetValidationResults()[0].ErrorMessage;
            var expect = "O sobrenome não pode estar em branco";
            Assert.AreEqual(expect, target);
        }

    }
}
