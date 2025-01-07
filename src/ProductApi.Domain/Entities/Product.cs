namespace ProductApi.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Update(Product product)
        {
            Name = product.Name;
            Price = product.Price;
        }
    }
}
