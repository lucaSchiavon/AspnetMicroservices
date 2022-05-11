namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        //copiamo qui il nome del prodotto per una questione di chiamate fra microservizi
        //e per evitare troppe chiamate
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}