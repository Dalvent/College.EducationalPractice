using LabaratoryWork7Part2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.Repository
{
    public class ProductFileRepository : IRepository<Product>
    {
        private const string FILE_NAME = "products.json";
        public ProductFileRepository(string fileName = FILE_NAME) { }
        public async Task Add(Product entity)
        {
            var products = (await ReadAllProductsFromStreamAsync()).ToList();
            Product lastElement = null;
            if(products.Count > 0)
            {
                lastElement = products.Aggregate((curMax, item) => item.Id > curMax.Id ? item : curMax);
            }
            int lastId = lastElement != null ? lastElement.Id : 1;
            entity.Id = lastId + 1;
            products.Add(entity);
            await UpdateFile(products);
        }
        public async Task Edit(Product entity)
        {
            var products = (await ReadAllProductsFromStreamAsync()).ToList();
            var indexToEdit = products.FindIndex(product => product.Id == entity.Id);
            products[indexToEdit] = entity;
            await UpdateFile(products);
        }
        public async Task<IQueryable<Product>> GetValues() => (await ReadAllProductsFromStreamAsync()).AsQueryable();
        public async Task Remove(Product entity)
        {
            var products = (await ReadAllProductsFromStreamAsync()).ToList();
            var indexToRemove = products.FindIndex(product => product.Id == entity.Id);
            products.RemoveAt(indexToRemove);
            await UpdateFile(products);
        }
        private async Task UpdateFile(List<Product> products)
        {
            using (var writer = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(writer, products, new JsonSerializerOptions() {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                });
            }
        }

        private async Task<List<Product>> ReadAllProductsFromStreamAsync()
        {
            using (var reader = new FileStream(FILE_NAME, FileMode.OpenOrCreate,FileAccess.Read))
            {
                try
                {
                    var products = await JsonSerializer.DeserializeAsync<Product[]>(reader);
                    return products.ToList();
                } catch (JsonException ex) { return new List<Product>(); }
                catch(Exception ex) { throw; }
            }
        }
    }
}
