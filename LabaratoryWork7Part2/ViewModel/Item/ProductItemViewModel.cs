using LabaratoryLib;
using LabaratoryWork7Part2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.ViewModel.Item
{
    public class ProductItemViewModel : BaseViewModel
    {
        public Product Product { get; }
        public ProductItemViewModel(Product product)
        {
            Product = product;
        }
        public string Name => Product.Name;
        public DateTime Date => Product.Date;
        public decimal Price => Product.Price;
        public int Count => Product.Count;
        public decimal Income => Price * Count;
    }
}
