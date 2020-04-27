using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormDB.Dto
{
    [Table("CUSTOMER", Schema = "QF")]
    public class CustomerDto : Entity
    {
        [Key] [Column("CustomerId")] public int CustomerId { get; set; }
        [Column("PersonalId")] public string PersonalId { get; set; }
        [Column("FirstName")] public string FirstName { get; set; }
        [Column("LastName")] public string LastName { get; set; }
        [Column("EMail")] public string EMail { get; set; }
        [Column("TelNumber1")] public string TelNumber1 { get; set; }
    }
}
