using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Exceptions
{
    public class ExceptionsForStudebtAndTeacher
    {
        public class TeacherNotFoundException : BaseExceptions
        {
            public TeacherNotFoundException(string className)
                : base($"לא קיימת כזאת כיתה בבית הספר '{className}'.", 404, "TCLASS_NOT_FOUND") { }
        }


        public class InvalidIdException : BaseExceptions
        {
            public InvalidIdException(string id)
                : base($"תעודת הזהות {id} אינה תקינה. .", 400, "INVALID_ID") { }
        }

        public class StudentAlreadyExistsException : BaseExceptions
        {
            public StudentAlreadyExistsException(string sId)
                : base($"התלמידה עם ת.ז {sId} כבר רשומה במערכת.", 409, "STUDENT_ALREADY_EXISTS") { }
        }

        public class TeacherAlreadyExistsException : BaseExceptions
        {
            public TeacherAlreadyExistsException(string tId)
                : base($"המורה עם תעודת זהות {tId} כבר קיימת במערכת.", 409, "TEACHER_ALREADY_EXISTS")
            {
            }
        }

        public class TeacherClassExistsException : BaseExceptions
        {
            public TeacherClassExistsException(string className)
                : base($"הכיתה '{className}' כבר תפוסה על ידי מורה אחרת", 409, "Teacher_Class_Exists_Exception")
            {
            }
        }
    }
}
