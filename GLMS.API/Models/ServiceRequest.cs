using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLMS.API.Models;


public class ServiceRequest
{
    [Key]
    public int RequestId { get; set; }

    [Required]
    public int ContractId { get; set; }
    public virtual Contract? Contract { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public decimal CostUSD { get; set; } // We'll convert this to ZAR later

    public decimal CostZAR { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";
}

