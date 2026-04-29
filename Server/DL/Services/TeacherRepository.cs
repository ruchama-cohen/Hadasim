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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly TripDbContext _context;

        public TeacherRepository()
        {
            _context = new TripDbContext();
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _context.Teachers.Find(teacher => true).ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(string id)
        {
            return await _context.Teachers.Find(teacher => teacher.TId == id).FirstOrDefaultAsync();
        }

        public async Task<Teacher> GetTeacherByClass(string ClassName)
        {
            return await _context.Teachers.Find(teacher => teacher.ClassName== ClassName).FirstOrDefaultAsync();
        }

        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.InsertOneAsync(teacher);
        }
    }
}
