using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.Model
{
    [Serializable]
    public class Product : BaseModel
    {
        public Product()
            : base(0)
        {
        }
        public Product(string name, DateTime date, decimal price, int count, int id = 0)
             : base(id)
        {
            Name = name;
            Date = date;
            Price = price;
            Count = count;
        }
        public Product(Product product, int id = 0)
            : this(product.Name, product.Date, product.Price, product.Count, id)
        {

        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
