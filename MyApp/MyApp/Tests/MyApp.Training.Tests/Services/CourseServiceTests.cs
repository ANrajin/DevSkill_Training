using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Exceptions;
using MyApp.Training.Repositories;
using MyApp.Training.Services;
using MyApp.Training.UnitOfWorks;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq.Expressions;
using CourseEntity = MyApp.Training.Entities.Course;

namespace MyApp.Training.Tests.Services
{
    public class CourseServiceTests
    {
        private AutoMock? _mock;
        private Mock<ICourseEnrollmentUnitOfWork>? _unitOfWork;
        private Mock<ICourseRepository>? _courseRepositoryMock;
        private Mock<IMapper>? _mapperMock;
        private ICourseService? _courseService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _unitOfWork = _mock?.Mock<ICourseEnrollmentUnitOfWork>();
            _courseRepositoryMock = _mock?.Mock<ICourseRepository>();
            _mapperMock = _mock?.Mock<IMapper>();
            _courseService = _mock?.Create<CourseService>();
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Reset();
            _courseRepositoryMock.Reset();
            _mapperMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock.Dispose();
        }

        [Test]
        public void CreateCourse_If_Course_Does_Not_Exists()
        {
            /*
             * Arrange
             */
            Course course = new Course
            {
                Name = "Test Course"
            };

            //verifying the Save() method is called
            _unitOfWork.Setup(x => x.Save()).Verifiable();

            _mapperMock.Setup(x => x.Map<CourseEntity>(course))
                .Returns(new CourseEntity
                {
                    Name = course.Name,
                });

            //making instance of course repository
            _unitOfWork?.Setup(c => c.Courses).Returns(_courseRepositoryMock.Object);

            //mocking the reult of GetCount() method of base Repository
            _courseRepositoryMock.Setup(
                c => c.GetCount(It.IsAny<Expression<Func<CourseEntity, bool>>>()))
                .Returns(0);

            //Verifing the Add() method of base repository
            _courseRepositoryMock.Setup(
                x => x.Add(It.Is<CourseEntity>(
                    y => y.Name == course.Name))).Verifiable();

            /**
             * Act
             */
            _courseService?.CreateCourse(course);


            /**
             * Assert
             */
            _unitOfWork.VerifyAll();
            _courseRepositoryMock.VerifyAll();
        }

        [Test]
        public void CreateCourse_Throws_Exception_If_Course_Already_Exists()
        {
            /*
             * Arrange
             */
            Course course = new Course
            {
                Name = "Test Course"
            };

            //making instance of course repository
            _unitOfWork?.Setup(c => c.Courses).Returns(_courseRepositoryMock.Object);

            //mocking the reult of GetCount() method of base Repository
            _courseRepositoryMock.Setup(
                c => c.GetCount(It.IsAny<Expression<Func<CourseEntity, bool>>>()))
                .Returns(1);

            /**
             * Act
             */
            Should.Throw<DuplicateException>(
                () => _courseService?.CreateCourse(course));
        }
    }
}