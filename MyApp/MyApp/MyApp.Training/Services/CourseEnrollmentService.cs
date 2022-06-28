using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEntity = MyApp.Training.Entities.Student;
using CourseStudentEntity = MyApp.Training.Entities.CourseStudent;

namespace MyApp.Training.Services
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly ICourseEnrollmentUnitOfWork _unitOfWork;

        public CourseEnrollmentService(IMapper mapper, ICourseEnrollmentUnitOfWork courseEnrollmentUnitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = courseEnrollmentUnitOfWork;
        }

        public void EnrollStudent(Course course, Student student)
        {
            var courseEntity = _unitOfWork.Courses.GetById(course.Id);
            var studentEntity = _unitOfWork.Students.GetById(student.Id);
            //Map<destination = Entity>(source = BusinessObject)
            _mapper.Map(student, studentEntity);

            CourseStudentEntity courseStudent = new CourseStudentEntity();
            courseStudent.Student = studentEntity;

            courseEntity.Students = new List<CourseStudentEntity>();
            courseEntity.Students.Add(courseStudent);

            _unitOfWork.Save();
        }
    }
}
