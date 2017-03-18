using CohortBuilder.DAL;
using CohortBuilder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CohortBuilder.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        public Mock<CohortContext> MockContext { get; set; }

        public Mock<DbSet<Cohort>> MockCohorts { get; set; }
        public Mock<DbSet<Instructor>> MockInstructors { get; set; }
        public Mock<DbSet<Student>> MockStudents { get; set; }

        public List<Cohort> CohortList { get; set; }
        public List<Instructor> InstructorList { get; set; }
        public List<Student> StudentList { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            MockContext = new Mock<CohortContext>();

            MockCohorts = new Mock<DbSet<Cohort>>();
            MockInstructors = new Mock<DbSet<Instructor>>();
            MockStudents = new Mock<DbSet<Student>>();

            CohortList = new List<Cohort>();
            InstructorList = new List<Instructor>();
            StudentList = new List<Student>();

            MockContext.Setup(x => x.Cohorts).Returns(MockCohorts.Object);
            MockContext.Setup(x => x.Students).Returns(MockStudents.Object);
            MockContext.Setup(x => x.Instructors).Returns(MockInstructors.Object);
        }

        public void SetUpMocks()
        {
            //Cohorts
            var cohortQueryable = CohortList.AsQueryable();

            MockCohorts.As<IQueryable<Cohort>>().Setup(x => x.Provider).Returns(cohortQueryable.Provider);
            MockCohorts.As<IQueryable<Cohort>>().Setup(x => x.Expression).Returns(cohortQueryable.Expression);
            MockCohorts.As<IQueryable<Cohort>>().Setup(x => x.ElementType).Returns(cohortQueryable.ElementType);
            MockCohorts.As<IQueryable<Cohort>>().Setup(x => x.GetEnumerator()).Returns(() => cohortQueryable.GetEnumerator());

            //Instructors
            var instructorsQueryable = InstructorList.AsQueryable();

            MockInstructors.As<IQueryable<Instructor>>().Setup(x => x.Provider).Returns(instructorsQueryable.Provider);
            MockInstructors.As<IQueryable<Instructor>>().Setup(x => x.Expression).Returns(instructorsQueryable.Expression);
            MockInstructors.As<IQueryable<Instructor>>().Setup(x => x.ElementType).Returns(instructorsQueryable.ElementType);
            MockInstructors.As<IQueryable<Instructor>>().Setup(x => x.GetEnumerator()).Returns(() => instructorsQueryable.GetEnumerator());

            //Students
            var studentsQueryable = StudentList.AsQueryable();

            MockStudents.As<IQueryable<Student>>().Setup(x => x.Provider).Returns(studentsQueryable.Provider);
            MockStudents.As<IQueryable<Student>>().Setup(x => x.Expression).Returns(studentsQueryable.Expression);
            MockStudents.As<IQueryable<Student>>().Setup(x => x.ElementType).Returns(studentsQueryable.ElementType);
            MockStudents.As<IQueryable<Student>>().Setup(x => x.GetEnumerator()).Returns(() => studentsQueryable.GetEnumerator());

        }

        [TestMethod]
        public void RepositoryCanBeInstantiated()
        {
            //Arrange
            var repo = new Repository();
            //Act
            //Assert
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepositoryHasContext()
        {
            //Arrange
            var repo = new Repository();
            //Act
            var context = repo.Context;
            //Assert
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void RepositoryHasContextWithDependencyInjection()
        {
            //Arrange
            var context = new CohortContext();
            var repo = new Repository(context);
            //Act
            var repoContext = repo.Context;
            //Assert
            Assert.IsNotNull(repoContext);
        }
    }
}

//Arrange
//Act
//Assert
