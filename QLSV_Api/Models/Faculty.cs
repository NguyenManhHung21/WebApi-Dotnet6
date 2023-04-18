using System;
using System.Collections.Generic;

namespace QLSV_Api.Models;

public partial class Faculty
{
    public int IdFaculty { get; set; }

    public string IndexFaculty { get; set; } = null!;

    public string NameFaculty { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
