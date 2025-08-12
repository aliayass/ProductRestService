using System.ComponentModel.DataAnnotations;
namespace API_Project.API.Models;


public class Product
{
    public int Id { get; set; }

    // max 10 karakter, sadece sayılar
    [Required]
    [MaxLength(10, ErrorMessage = "Barkod en fazla 10 karakter olabilir.")]
    [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Barkod yalnızca sayılardan oluşmalıdır.")]
    public string Barkod { get; set; }

    // max 10 karakter, küçük harf olmasın
    [Required]
    [MaxLength(10, ErrorMessage = "ItemId en fazla 10 karakter olabilir.")]
    [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "ItemId yalnızca büyük harf ve rakamlardan oluşmalıdır.")]
    public string ItemId { get; set; }

    public string Renk { get; set; }

    public string Beden { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int StokSayisi { get; set; }
}
