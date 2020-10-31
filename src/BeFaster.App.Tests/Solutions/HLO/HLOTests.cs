using BeFaster.App.Solutions.HLO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.HLO
{
    public class HLOTests
    {
        [TestCase("James Brown", "Hello James Brown")]
        public void GivenAFriendlyNameShouldReturnAPersonalisedHello(string name, string expected)
        {
            // ACT
            var result = HelloSolution.Hello(name);

            // ASSERT
            Assert.AreEqual(expected, result);
        }
    }
}




