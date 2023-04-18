using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV_Api.Models;

namespace QLSV_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly QlsinhvienContext _context;

        public StudentsController(QlsinhvienContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
          if (_context.Students == null)
          {
              return NotFound();
          }
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("findStd-{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                if (_context.Students == null)
                {
                    return NotFound();
                }
                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.IdStd)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Route("addStd")]
        //public async Task<ActionResult<IEnumerable<ModelInfo>>> PostStudent(CreateStudentDto student)
        //{
        //    var faculty = await _context.Faculties.FindAsync(student.IdFaculty);
        //    if(faculty == null)
        //    {
        //        return NotFound();
        //    }

        //    var newStudent = new Student
        //    {
        //        NameStd = student.NameStd,
        //        Birthday = student.Birthday,
        //        Gender = student.Gender,
        //        IdFacultyNavigation = faculty
        //    };

        //    _context.Students.Add(newStudent);
        //    await _context.SaveChangesAsync();

        //    return await GetStdAndFac();

        //    //if (_context.Students == null)
        //    //{
        //    //    return Problem("Entity set 'QlsinhvienContext.Students'  is null.");
        //    //}
        //    //  _context.Students.Add(student);
        //    //  await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetStudent", new { id = student.IdStd }, student);
        //}

        // "nameStd": "hahahaha",
        //"birthday": "2023-04-14",
        //"gender": "Nu",
        //"idFaculty": 2,
        [HttpPost]
        [Route("addDefault")]
        public async Task<ActionResult> PostStudent(Student student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'QlsinhvienContext.Students'  is null.");
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.IdStd }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("delStudent-{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
                
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return await _context.Students.ToListAsync();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.IdStd == id)).GetValueOrDefault();
        }

        //GET /id, name, birthday, gender, faculty
        [HttpGet]
        [Route("listStd")]
        public async Task<ActionResult<IEnumerable<ModelInfo>>> GetStdAndFac()
        {
            var stdList = from std in _context.Students
                          join fac in _context.Faculties on std.IdFaculty equals fac.IdFaculty
                          select new ModelInfo()
                          {
                              IdStd = std.IdStd,
                              NameStd = std.NameStd,
                              Birthday = std.Birthday,
                              Gender = std.Gender,
                              IdFaculty = fac.IdFaculty,
                              NameFaculty = fac.NameFaculty
                          };
            return stdList.ToList();
        }

        [HttpDelete]
        [Route("delCheckBox")]
        public async Task<ActionResult<IEnumerable<Student>>> DelCheckBox(int[] idList)
        {
            List<Student> delList = new List<Student>();
            try
            {
            foreach (var id in idList)
            {
                var stdDel = await _context.Students.FindAsync(id);
                delList.Add(stdDel!);
            }
                _context.Students.RemoveRange(delList);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return await _context.Students.ToListAsync();
        }
    }
}
