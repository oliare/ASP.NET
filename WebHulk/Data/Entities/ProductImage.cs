namespace WebHulk.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public required Product Product { get; set; }

    }
}
