using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.Exceptions;
using FirstDemo.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEntity = FirstDemo.Training.Entities.Course;
using StudentEntity = FirstDemo.Training.Entities.Student;
using CourseStudentEntity = FirstDemo.Training.Entities.CourseStudent;

namespace FirstDemo.Training.Services
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly ICourseEnrollementUnitOfWork _courseEnrollementUnitOfWork;
        private readonly IMapper _mapper;

        public CourseEnrollmentService(IMapper mapper,
            ICourseEnrollementUnitOfWork courseEnrollementUnitOfWork)
        {
            _courseEnrollementUnitOfWork = courseEnrollementUnitOfWork;
            _mapper = mapper;
        }

        public void EnrollStudent(Course course, Student student)
        {
            var courseEntity = _courseEnrollementUnitOfWork.Courses.GetById(course.Id);
            var studentEntity = _courseEnrollementUnitOfWork.Students.GetById(student.Id);

            _mapper.Map(student, studentEntity);

            CourseStudentEntity courseStudent = new CourseStudentEntity();
            courseStudent.Student = studentEntity;

            courseEntity.Students = new List<CourseStudentEntity>();
            courseEntity.Students.Add(courseStudent);

            _courseEnrollementUnitOfWork.Save();
        }
    }
}
