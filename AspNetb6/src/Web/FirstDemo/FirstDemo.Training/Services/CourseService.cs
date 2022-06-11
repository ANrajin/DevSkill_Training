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

namespace FirstDemo.Training.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseEnrollementUnitOfWork _courseEnrollementUnitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, 
            ICourseEnrollementUnitOfWork courseEnrollementUnitOfWork)
        {
            _courseEnrollementUnitOfWork = courseEnrollementUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCourse(Course course)
        {
            var courseCount = _courseEnrollementUnitOfWork.Courses
                .GetCount(x => x.Name == course.Name);

            if (courseCount == 0)
            {
                var entity = _mapper.Map<CourseEntity>(course);

                _courseEnrollementUnitOfWork.Courses.Add(entity);
                _courseEnrollementUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Course with same name already exists");
        }

        public void DeleteCourse(int id)
        {
            _courseEnrollementUnitOfWork.Courses.Remove(id);
            _courseEnrollementUnitOfWork.Save();
        }

        public void EditCourse(Course course)
        {
            var courseCount = _courseEnrollementUnitOfWork.Courses
                .GetCount(x => x.Name == course.Name
                && x.Id != course.Id);

            if (courseCount == 0)
            {
                var courseEntity = _courseEnrollementUnitOfWork.Courses.GetById(course.Id);

                courseEntity = _mapper.Map(course, courseEntity);

                _courseEnrollementUnitOfWork.Save();
            }
            else
                throw new DuplicateException("Course name already exists");
        }

        public Course GetCourse(int id)
        {
            var courseEntity = _courseEnrollementUnitOfWork.Courses.GetById(id);
            
            var course = _mapper.Map<Course>(courseEntity);

            return course;
        }

        public (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, 
            int pageSize, string searchText, string orderBy)
        {
            var result = _courseEnrollementUnitOfWork.Courses.GetDynamic(x => x.Name.Contains(searchText),
                orderBy, string.Empty, pageIndex, pageSize, true);

            List<Course> courses = new List<Course>();
            foreach(CourseEntity course in result.data)
            {
                courses.Add(_mapper.Map<Course>(course));
            }

            return (result.total, result.totalDisplay, courses);
        }

        public IList<Course> GetCourses()
        {
            var courseEntities = _courseEnrollementUnitOfWork.Courses.GetAll();

            List<Course> courses = new List<Course>();
            
            foreach(CourseEntity entity in courseEntities)
            {
                courses.Add(_mapper.Map<Course>(entity));
            }

            return courses;
        }
    }
}
