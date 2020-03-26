using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD3.DAL;
using APBD3.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) {

            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2) {
                return Ok("Malewski");
            }
            return NotFound("Student not found");

        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id) {


            if (id == 1) { return Ok("Update completed"); }
            else return NotFound("Student not found");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {


            if (id == 1) { return Ok("Delete completed"); }
            else return NotFound("Student not found");
        }
    }
}   