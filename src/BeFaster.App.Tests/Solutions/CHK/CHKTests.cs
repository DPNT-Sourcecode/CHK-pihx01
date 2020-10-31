﻿using BeFaster.App.Solutions.CHK;
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
        private List<Product> _products;
        private List<SpecialOffer> _specialOffers;
        private CheckoutSolution _checkoutSolution;

        [SetUp]
        public void SetUp()
        {
            _products = new List<Product>()
            {
                new Product{ SKU = "A", Price = 50},
                new Product{ SKU = "B", Price = 30},
                new Product{ SKU = "C", Price = 20},
                new Product{ SKU = "D", Price = 15}
            };

            _specialOffers = new List<SpecialOffer>()
            { 
                new SpecialOffer{ SKU= "A", Quantity=3, Value = 20 },
                new SpecialOffer{ SKU= "B", Quantity=2, Value = 15 }
            };
        }

        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void GivenAnSKUShouldCalculateCorrectPrice(string sku, int expected)
        {
            // ARRANGE

            // ACT
            var result = _checkoutSolution.ComputePrice(sku);

            // ASSERT
            Assert.AreEqual(expected, result);
        }
    }
}


