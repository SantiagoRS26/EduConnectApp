using System;
using System.Collections.Generic;

namespace EduConnect.Models;

public partial class Department
{
    public string DepartmentId { get; set; } = null!;

    public string? DepartmentName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
