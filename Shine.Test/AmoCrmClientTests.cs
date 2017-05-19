#region Usings

using System.Threading.Tasks;

using AmoCrm.Client;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Shine.Tests
{
    [TestClass]
    public class AmoCrmClientTests
    {
        [TestMethod]
        public void AuthTest()
        {
            // ARRANGE
            var amoClient = new AmoCrmClient();

            // ACT
            var response = amoClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");

            // ASSERT
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetContactByEmailTest()
        {
            // ARRANGE
            var amoClient = new AmoCrmClient();

            // ACT
            amoClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            var contact = amoClient.GetContactByEmail("soa@shine.city");

            // ASSERT
            Assert.IsNotNull(contact);
        }

        [TestMethod]
        public void AddLeadTest()
        {
            var amoClient = new AmoCrmClient();

            // ACT
            amoClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            var contact = amoClient.AddLead("Test", "123");

            // ASSERT
            Assert.IsNotNull(contact);
        }


        [TestMethod]
        public void GetCurrentAccountTest()
        {
            // ARRANGE
            var amoClient = new AmoCrmClient();

            // ACT
            amoClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            var account = amoClient.GetCurrentAccount();

            // ASSERT
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void GetpipeLinesTest()
        {
            // ARRANGE
            var amoClient = new AmoCrmClient();

            // ACT
            amoClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            var pipelines = amoClient.GetpipeLines();

            // ASSERT
            Assert.IsNotNull(pipelines);
        }
    }
}