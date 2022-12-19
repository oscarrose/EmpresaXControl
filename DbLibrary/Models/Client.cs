using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbLibrary.Models;

public partial class Client
{
    [Key]
    public int ClientId { get; set; }
    [Required(ErrorMessage = "The name is required.")]
    public string ClientName { get; set; } = null!;
    [Required(ErrorMessage = "The last name is required.")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "The identification is required.")]
    public string Identification { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? StatusClient { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual IList<ClientAddress> ClientAddresses { get; set; } = new List<ClientAddress>();
}
