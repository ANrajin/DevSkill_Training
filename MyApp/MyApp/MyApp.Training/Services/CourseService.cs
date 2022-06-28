using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Exceptions;
using MyApp.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEntity = MyApp.Training.Entities.Course;

namespace MyApp.Training.Services
{
    public class CourseService : ICourseService
    {
        private readonly  ICourseEnrollmentUnitOfWork _courseEnrollmentUnitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper,ICourseEnrollmentUnitOfWork courseEnrollmentUnitOfWork)
        {
            _courseEnrollmentUnitOfWork = courseEnrollmentUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCourse(Course course)
        {
            //Check if same name course already exist in database
            var courseCount = _courseEnrollmentUnitOfWork.Courses.GetCount(x => x.Name == course.Name);

            if (courseCount == 0)
            {
                //Map using Auto Mapper
                //_mapper.Map<dest>(src)
                var entity = _mapper.Map<CourseEntity>(course);

                _courseEnrollmentUnitOfWork.Courses.Add(entity);
                _courseEnrollmentUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Course with same name already exists");
        }

        public void DeleteCourse(int id)
        {
            _courseEnrollmentUnitOfWork.Courses.Remove(id);
            _courseEnrollmentUnitOfWork.Save();
        }

        public IList<Course> GetAllCourses()
        {
            var courseEntities = _courseEnrollmentUnitOfWork.Courses.GetAll();
            IList<Course> courseList = new List<Course>();

            foreach (CourseEntity entity in courseEntities)
            {
                courseList.Add(_mapper.Map<Course>(entity));
            }

            return courseList;
        }

        public Course GetCourse(int id)
        {
            var courseEntity = _courseEnrollmentUnitOfWork.Courses.GetById(id);
            var course = _mapper.Map<Course>(courseEntity);

            return course;
        }

        public (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderBy)
        {
            var result = _courseEnrollmentUnitOfWork.Courses.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);

            List<Course> courses = new List<Course>();
            foreach (CourseEntity course in result.data)
            {
                courses.Add(_mapper.Map<Course>(course));
            }

            return (result.total, result.totalDisplay, courses);
        }

        public void UpdateCourse(Course course)
        {
            //check if new data is already matching with any other row/data except itself
            var courseCount = _courseEnrollmentUnitOfWork.Courses.GetCount(
                x => x.Name == course.Name && x.Id != course.Id);

            if (courseCount == 0)
            {
                var courseEntity = _courseEnrollmentUnitOfWork.Courses.GetById(course.Id);
                courseEntity = _mapper.Map(course, courseEntity);

                _courseEnrollmentUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Course name already exists");
        }
    }
}
