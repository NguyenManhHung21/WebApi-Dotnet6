using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QLSV_Api.Models;

public partial class Student
{
    public int IdStd { get; set; }

    public string NameStd { get; set; } = null!;

    public DateTime? Birthday { get; set; }
    public string? Gender { get; set; }
    public int? IdFaculty { get; set; }
    public virtual Faculty? IdFacultyNavigation { get; set; }
}
