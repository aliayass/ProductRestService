using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SOAPProduct.Models
{
    [DataContract]
    public class Product
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Barkod { get; set; } = string.Empty;

        [Required]
        [DataMember]
        public string ItemId { get; set; } = string.Empty;

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Beden { get; set; } = string.Empty;

        [DataMember]
        public string Renk { get; set; } = string.Empty;
    }
}