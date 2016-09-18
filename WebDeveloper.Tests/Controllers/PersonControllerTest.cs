using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebDeveloper.Areas.Personnel.Controllers;
using WebDeveloper.Areas.Personnel.Models;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using Xunit;

namespace WebDeveloper.Tests.Controllers
{
    public class PersonControllerTest
    {
        private PersonController controller;
        private IRepository<Person> _repository;
        private Mock<DbSet<Person>> personDbSetMock;
        private Mock<WebContextDb> webContextMock;

        [Fact(DisplayName = "ListActionEmptyParametersTest")]
        private void ListActionEmptyParametersTest()
        {
            ListConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.List(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_List");

            var modelCount = (IEnumerable<Person>)result.Model;
            modelCount.Count().Should().Be(10);
        }

        [Fact(DisplayName = "CreateGetTest")]
        private void CreateGetTest()
        {
            BasicConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Create() as PartialViewResult;
            result.ViewName.Should().Be("_Create");

            var personModelCreate = (PersonViewModel)result.Model;
            personModelCreate.Person.Should().NotBeNull();
        }

        [Fact(DisplayName = "CreatePostTestOk")]
        private void CreatePostTestOk()
        {
            BasicConfigMockData();
            controller = new PersonController(_repository);
            var result = controller.Create(TestPersonOk()) as PartialViewResult;
            result.Should().BeNull();

            personDbSetMock.Verify(s => s.Add(It.IsAny<Person>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "CreatePostTestWrong")]
        private void CreatePostTestWrong()
        {
            BasicConfigMockData();
            controller = new PersonController(_repository);
            var personToFail = TestPersonWrong();
            controller.ModelState.AddModelError("errorTest", "errorTest");
            var result = controller.Create(personToFail) as PartialViewResult;
            result.ViewName.Should().Be("_Create");

            var personModelCreate = (PersonViewModel)result.Model;
            personModelCreate.Person.Should().Be(personToFail);

        }

        #region Configuration Values
        public void PersonMockList()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Person
            {
                PersonType = "SC",
                FirstName = $"Name{i}",
                LastName = $"LastName{i}",
                ModifiedDate = DateTime.Now,
            }).AsQueryable();
            personDbSetMock = new Mock<DbSet<Person>>();
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }
        private Person TestPersonOk()
        {
            var person = new Person
            {
                PersonType = "SC",
                FirstName = "Test",
                LastName = "Test",
                EmailPromotion = 1
            };
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {
                ModifiedDate = DateTime.Now,
                rowguid = person.rowguid
            };
            return person;
        }
        private Person TestPersonWrong()
        {
            var person = new Person
            {
                FirstName = "Wrong",
                LastName = "Wrong"
            };
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            return person;
        }
        private void ListConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();
            PersonMockList();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
            controller = new PersonController(_repository);
        }
        private void BasicConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
            controller = new PersonController(_repository);
        }
        #endregion
    }
}
