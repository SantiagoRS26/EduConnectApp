using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class College
{
    public Guid CollegeId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? AdditionalInfo { get; set; }

    public int? AvailableSlots { get; set; }

    public string? Department { get; set; }

    public string? City { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
