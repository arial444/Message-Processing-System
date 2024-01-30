using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceA.Models
{
    [Table("randomNumber", Schema = "dbo")]
    public class RandomNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("number_id")]
        public int ID { get; set; }
        [Column("number")]
        public int Number { get; set; }
    }
}