using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbLibrary.Models;

public partial class ClientAddress
{
    [Key]
    public int AddressesId { get; set; }
    [ForeignKey("Client"), Column(Order = 1)]
    public int ClientId { get; set; }

    public string Country { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string? PostalCode { get; set; }

    public int? HouseNumber { get; set; }

    public virtual Client? Client { get; set; } = null!;
}
