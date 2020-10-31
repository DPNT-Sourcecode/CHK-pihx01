using BeFaster.App.Solutions.CHK;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class CHKTests
    {   
        private ICheckout _checkoutSolution;

        [SetUp]
        public void SetUp()
        {
            _checkoutSolution = new CheckoutSolution();          
        }

        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        [TestCase("E", 40)]
        [TestCase("a", -1)]
        [TestCase("b", -1)]
        [TestCase("x", -1)]
        [TestCase("-", -1)]
        [TestCase("", 0)]
        public void GivenAnSKUShouldCalculateCorrectPrice(string sku, int expected)
        {
            // ARRANGE

            // ACT
            var result = _checkoutSolution.ComputePrice(sku);

            // ASSERT
            Assert.AreEqual(expected, result);
        }

        [TestCase("AAA", 130)]
        [TestCase("AAAAA", 200)]
        [TestCase("AAAAAA", 250)]
        [TestCase("AAAAAAA", 300)]
        [TestCase("AAAAAAAA", 380)]
        [TestCase("BB", 45)]
        [TestCase("EE", 40)]
        [TestCase("CCC", 60)]
        [TestCase("AAa", -1)]
        [TestCase("AxA", -1)]
        [TestCase("ABCa", -1)]
        public void GivenAListOfProductsShouldApplyDiscount(string sku, int expected)
        {
            // ARRANGE

            // ACT
            var result = _checkoutSolution.ComputePrice(sku);

            // ASSERT
            Assert.AreEqual(expected, result);
        }        
    }
}

