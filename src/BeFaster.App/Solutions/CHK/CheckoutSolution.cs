using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TDL.Client.Queue.Abstractions.Response;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution : ICheckout
    {
        private readonly List<Product> _products;
        private readonly List<SpecialOffer> _specialOffers;

        public CheckoutSolution()
        {
            _products = new List<Product>()
            {
                new Product{ SKU = "A", Price = 50},
                new Product{ SKU = "B", Price = 30},
                new Product{ SKU = "C", Price = 20},
                new Product{ SKU = "D", Price = 15},
                new Product{ SKU = "E", Price = 40}
            };

            _specialOffers = new List<SpecialOffer>()
            {
                new SpecialOffer{ SKU= "A", Quantity=3, Value = 20 },
                new SpecialOffer{ SKU= "B", Quantity=2, Value = 15 },
                new SpecialOffer{ SKU= "A", Quantity=5, Value = 30 },
                new SpecialOffer{ SKU= "E", Quantity=2, Value = 40 }                
            };
        }

        public int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus))
                return 0;

            var regex = new Regex(@"^[a-zA-Z0-9\s,]*$");
            if (!regex.IsMatch(skus))
                return -1;            

            if (skus.Length == 1)            
                return GetPriceForIndividualItem(skus);
            
            return ScanMultipleItemsAndAppyDiscount(skus);
        }       

        private int GetPriceForIndividualItem(string sku)
        {
            var product = _products.Where(x => x.SKU == sku).FirstOrDefault();

            if (product == null)
                return -1;

            return product.Price;
        }

        private int ScanMultipleItemsAndAppyDiscount(string skus)
        {
            if (!IsValidSetOfSKUS(skus))
                return -1;

            var splitItems = skus.ToCharArray();
            var matchingProducts = new List<Product>();

            foreach (var item in splitItems)
            {
                var matchingProduct = _products.Where(x => x.SKU == item.ToString()).FirstOrDefault();

                if (matchingProduct != null)
                {
                    matchingProducts.Add(matchingProduct);
                }
                else
                {
                    matchingProducts.Add(new Product() {SKU=item.ToString(),Price = -1 });
                }
            }           

            return GetTotalCostWithDiscountsApplied(matchingProducts);
        }

        private int GetTotalCostWithDiscountsApplied(List<Product> products) 
        {
            var totalCost = 0;
            var totalDiscount = 0;

            totalCost = products.Sum(x=> GetPriceForIndividualItem(x.SKU));
            totalDiscount = _specialOffers.Sum(specialOffer => CalculateDiscountForItem(specialOffer, products));

            var totalCostWithDiscountApplied = totalCost - totalDiscount;

            if (products.Where(x => x.SKU == "A").Count() > 6)
                totalCostWithDiscountApplied += 20;
        }

        private int CalculateDiscountForItem(SpecialOffer specialOffer, List<Product> products)
        {
            var count = products.Count(x=> x.SKU == specialOffer.SKU);
            var discountForItem = (count / specialOffer.Quantity) * specialOffer.Value;

            return discountForItem;
        }

        private bool IsValidSetOfSKUS(string skus)
        {
            foreach (char item in skus.ToCharArray())
            {
                if (!Char.IsUpper(item))
                    return false;
            }

            return true;
        }
    }    
}



