using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Models;
using DL.API;
using MongoDB.Driver;

namespace DL.Services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TripDbContext _context;

        public StudentRepository()
        {
            _context = new TripDbContext();
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.Find(student => true).ToListAsync();
        } 
        
        public async Task<Student> GetStudentById(string id)
        {
            return await _context.Students.Find(student => student.SId == id).FirstOrDefaultAsync();
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.InsertOneAsync(student);
        }


        public async Task<List<Student>> GetByClassAsync(string className)
        {
            return await _context.Students.Find(s => s.ClassName == className).ToListAsync();
        }
    }
}
