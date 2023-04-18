namespace QLSV_Api
{
    public class CreateStudentDto
    {
        public string NameStd { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        public string? Gender { get; set; }

        public int? IdFaculty { get; set; }
    }
}
