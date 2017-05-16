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
            AmoResponse response = null;
            //var r = AmoCrmClient.Auth("maa@shine.city", "b526e7df7b1f22b34a3b8a1d3b7530f0");
            Assert.IsNotNull(response);
        }

        public void GetLeadByEmailTest()
        {
            
        }
    }
}