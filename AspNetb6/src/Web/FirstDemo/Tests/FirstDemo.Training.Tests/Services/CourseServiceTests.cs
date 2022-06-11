using Autofac.Extras.Moq;
using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.Exceptions;
using FirstDemo.Training.Repositories;
using FirstDemo.Training.Services;
using FirstDemo.Training.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq.Expressions;
using CourseEO = FirstDemo.Training.Entities.Course;

namespace FirstDemo.Training.Tests
{
    public class CourseServiceTests
    {
        private AutoMock _mock;
        private Mock<ICourseEnrollementUnitOfWork> _courseEnrollementUnitOfWork;
        private Mock<ICourseRepository> _courseRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private ICourseService _courseService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _courseEnrollementUnitOfWork = _mock.Mock<ICourseEnrollementUnitOfWork>();
            _courseRepositoryMock = _mock.Mock<ICourseRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _courseService = _mock.Create<CourseService>();
        }

        [TearDown]
        public void TearDown()
        {
            _courseEnrollementUnitOfWork.Reset();
            _courseRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void CreateCourse_CourseDoesNotExists_CreatesCourse()
        {
            // Arrange
            Course course = new Course
            {
                Name = "ABC"
            };

            _courseEnrollementUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetCount(
                It.IsAny<Expression<Func<CourseEO, bool>>>())).Returns(0);

            _mapperMock.Setup(x => x.Map<CourseEO>(course))
                .Returns(new CourseEO() { Name = course.Name});

            _courseEnrollementUnitOfWork.Setup(x => x.Save()).Verifiable();
            _courseRepositoryMock.Setup(x => x.Add(It.Is<CourseEO>(y => y.Name == course.Name)))
                .Verifiable();

            // Act
            _courseService.CreateCourse(course);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _courseEnrollementUnitOfWork.VerifyAll(),
                () => _courseRepositoryMock.VerifyAll()
            );

        }

        [Test]
        public void CreateCourse_CourseExists_ThrowsError()
        {
            // Arrange
            Course course = new Course
            {
                Name = "ABC"
            };

            _courseEnrollementUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetCount(
                It.IsAny<Expression<Func<CourseEO, bool>>>())).Returns(1);

            // Act
            Should.Throw<DuplicateException>(
                () => _courseService.CreateCourse(course)
            );
        }

        [Test]
        public void GetCourse_ValidId_ReturnsCourse()
        {
            // Arrange
            var id = 3;

            CourseEO courseEntity = new CourseEO
            {
                Id = 3
            };
            Course course = new Course() { Id = courseEntity.Id };

            _courseEnrollementUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetById(id)).Returns(courseEntity);

            _mapperMock.Setup(x => x.Map<Course>(courseEntity))
                .Returns(course);

            // Act
            var result = _courseService.GetCourse(id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Id.ShouldBe(id)
            );
        }
    }
}