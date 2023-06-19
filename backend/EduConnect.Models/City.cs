using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class City
{
    public string CityId { get; set; } = null!;

    public string? CityName { get; set; }

    public string? DepartmentId { get; set; }

    public virtual ICollection<College> Colleges { get; set; } = new List<College>();

    public virtual Department? Department { get; set; }
}
