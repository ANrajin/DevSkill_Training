using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Exceptions;
using MyApp.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEntity = MyApp.Training.Entities.Student;

namespace MyApp.Training.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly ICourseEnrollmentUnitOfWork _courseEnrollmentUnitOfWork;

        public StudentService(IMapper mapper, ICourseEnrollmentUnitOfWork courseEnrollmentUnitOfWork)
        {
            _mapper = mapper;
            _courseEnrollmentUnitOfWork = courseEnrollmentUnitOfWork;
        }

        public void CreateStudent(Student student)
        {
            var studentCount = _courseEnrollmentUnitOfWork
                .Students.GetCount(s => s.Name == student.Name);

            if(studentCount == 0)
            {
                var entity = _mapper.Map<StudentEntity>(student);
                _courseEnrollmentUnitOfWork.Students.Add(entity);
                _courseEnrollmentUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Student with same name already exists");
        }

        public void DeleteStudent(int id)
        {
            _courseEnrollmentUnitOfWork.Students.Remove(id);
            _courseEnrollmentUnitOfWork.Save();
        }

        public (int total, int totalDisplay, IList<Student> records) 
            GetStudents(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = _courseEnrollmentUnitOfWork.Students.GetDynamic((x => x.Name.Contains(searchText)),
                orderBy, string.Empty, pageIndex, pageSize, true);

            List<Student> students = new List<Student>();
            foreach (StudentEntity student in result.data)
            {
                students.Add(_mapper.Map<Student>(student));
            }

            return (result.total, result.totalDisplay, students);
        }

        public Student GetStudent(int id)
        {
            var studentEntity = _courseEnrollmentUnitOfWork.Students.GetById(id);
            return _mapper.Map<Student>(studentEntity);
        }

        public void UpdateStudent(Student student)
        {
            var studentCount = _courseEnrollmentUnitOfWork
                .Students.GetCount(s => s.Name == student.Name && s.Id != student.Id);

            if (studentCount == 0)
            {
                var studentEntity = _courseEnrollmentUnitOfWork.Students.GetById(student.Id);
                studentEntity = _mapper.Map(student, studentEntity);

                _courseEnrollmentUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Student name already exists");
        }

        public IList<Student> GetAllStudents()
        {
            var studentEntities = _courseEnrollmentUnitOfWork.Students.GetAll();
            IList<Student> studentList = new List<Student>();

            foreach (StudentEntity entity in studentEntities)
            {
                studentList.Add(_mapper.Map<Student>(entity));
            }

            return studentList;
        }
    }
}
