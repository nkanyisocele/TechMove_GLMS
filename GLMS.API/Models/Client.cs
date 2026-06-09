using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
namespace GLMS.API.Models;




public class Client
{
    [Key]
    public int ClientId { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    public required string ContactDetails { get; set; }

    [Required]
    public required string Region { get; set; }

    // This links to the Contracts table
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
