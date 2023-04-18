namespace QLSV_Api.Models
{
    public class ModelInfo
    {
        public int IdStd { get; set; }

        public string NameStd { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        public string? Gender { get; set; }
        public int IdFaculty { get; set; }

        public string NameFaculty { get; set; } = null!;
    }
}
