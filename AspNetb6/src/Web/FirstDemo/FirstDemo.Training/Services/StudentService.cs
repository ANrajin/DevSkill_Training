using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEntity = FirstDemo.Training.Entities.Student;

namespace FirstDemo.Training.Services
{
    public class StudentService : IStudentService
    {
        private readonly ICourseEnrollementUnitOfWork _courseEnrollementUnitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IMapper mapper,
            ICourseEnrollementUnitOfWork courseEnrollementUnitOfWork)
        {
            _courseEnrollementUnitOfWork = courseEnrollementUnitOfWork;
            _mapper = mapper;
        }

        public void CreateStudent(Student student)
        {
            var studentCount = _courseEnrollementUnitOfWork.Students
                .GetCount(x => x.Name == student.Name);

            var entity = _mapper.Map<StudentEntity>(student);

            _courseEnrollementUnitOfWork.Students.Add(entity);
            _courseEnrollementUnitOfWork.Save();
        }

        public void DeleteStudent(int id)
        {
            _courseEnrollementUnitOfWork.Students.Remove(id);
            _courseEnrollementUnitOfWork.Save();
        }

        public void EditStudent(Student student)
        {
            var studentEntity = _courseEnrollementUnitOfWork.Students.GetById(student.Id);

            studentEntity = _mapper.Map(student, studentEntity);

            _courseEnrollementUnitOfWork.Save();
        }

        public Student GetStudent(int id)
        {
            var studentEntity = _courseEnrollementUnitOfWork.Students.GetById(id);

            var student = _mapper.Map<Student>(studentEntity);

            return student;
        }

        public async Task<(int total, int totalDisplay, IList<Student> records)> GetStudentsAsync(int pageIndex,
            int pageSize, string searchName, string searchAddress, string orderBy)
        {
            var result = await _courseEnrollementUnitOfWork.Students.GetStudentsAsync(
                pageIndex,
                pageSize,
                orderBy,
                searchName,
                searchAddress);

            List<Student> students = new List<Student>();
            foreach (StudentEntity student in result.data)
            {
                students.Add(_mapper.Map<Student>(student));
            }

            return (result.total, result.totalFiltered, students);
        }

        public IList<Student> GetStudents()
        {
            var studentEntities = _courseEnrollementUnitOfWork.Students.GetAll();

            List<Student> students = new List<Student>();

            foreach (StudentEntity entity in studentEntities)
            {
                students.Add(_mapper.Map<Student>(entity));
            }

            return students;
        }
    }
}
