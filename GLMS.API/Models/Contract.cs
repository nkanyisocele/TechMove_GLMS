using GLMS.Web.Models;
using System.ComponentModel.DataAnnotations;
namespace GLMS.API.Models;


public class Contract
{
    [Key]
    public int ContractId { get; set; }

    [Required]
    public int ClientId { get; set; }
    public virtual Client? Client { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Status { get; set; } = "Draft"; // Draft, Active, Expired, On Hold

    public string? ServiceLevel { get; set; }

    // This will store the PDF filename later (Rubric Item 4)
    public string? SignedAgreementFileName { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}

