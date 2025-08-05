namespace API_Project.API.Dto
{
    public class ProductDto
    {
        public int id { get; set; }
        public string barkod { get; set; }
        public string itemId { get; set; }
        public string renk { get; set; }
        public string beden { get; set; }
        public decimal price { get; set; }
        public int stokSayisi { get; set; }
    }
}

