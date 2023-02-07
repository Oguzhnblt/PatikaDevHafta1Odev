using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatikaDevHafta1Odev.Entities
{

    public class Product
    {
        [Required]
        [Column(Order = 1)]  // Tablonun her zaman 1 numaralı ID'den başlaması için
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }


}
