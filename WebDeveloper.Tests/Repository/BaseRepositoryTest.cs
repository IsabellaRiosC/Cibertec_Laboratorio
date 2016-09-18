using Xunit;
using FluentAssertions;
using WebDeveloper.Repository;
using WebDeveloper.Model;
using System;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace WebDeveloper.Tests.Repository
{

    public class BaseRepositoryTest
    {
        private IRepository<Person> _repository;
        private Mock<DbSet<Person>> personDbSetMock;
        private Mock<WebContextDb> webContextMock;

        [Fact(DisplayName = "AddTestOk")]
        public void AddTestOk()
        {
            BasicConfigMockData();
            var newPerson = TestPersonOk();
            var result = _repository.Add(newPerson);

            personDbSetMock.Verify(s => s.Add(It.IsAny<Person>()), Times.Once());
            //    webContextMock.Verify(c => c.SaveChanges(), Times.Once());
            webContextMockVerify();
        }

        [Fact(DisplayName = "AddTestWrong")]
        public void AddTestWrong()
        {
            BasicConfigMockData();
            var person = new Person();
            person.rowguid = Guid.NewGuid();
            try
            {
                _repository.Add(person);
            }
            catch (Exception ex)
            {
                ex.Source.Should().Be("EntityFramework");
            }
        }

        [Fact(DisplayName = "EditTestOk")]
        public void EditTestOk()
        {
            ListConfigMockData();
            var personToUpdate = _repository.GetById(x => x.FirstName == "Name1");
            var result = _repository.Update(personToUpdate);
            //webContextMock.Verify(c => c.SaveChanges(), Times.Once());
            webContextMockVerify();
        }

        [Fact(DisplayName = "DeleteTestOk")]
        public void DeleteTestOk()
        {
            ListConfigMockData();
            var personToUpdate = _repository.GetById(x => x.FirstName == "Name1");
            var result = _repository.Delete(personToUpdate);
            //webContextMock.Verify(c => c.SaveChanges(), Times.Once());
            webContextMockVerify();
        }

        [Fact(DisplayName = "GetListTestOk")]
        public void GetListTestOk()
        {
            ListConfigMockData();
            var result = _repository.GetList();
            result.Count.Should().BeGreaterOrEqualTo(10);
        }

        [Fact(DisplayName = "GetByIdTestOk")]
        public void GetByIdTestOk()
        {
            ListConfigMockData();
            var person = _repository.GetById(x => x.FirstName == "Name1");
            person.Should().NotBeNull();
        }

        [Fact(DisplayName = "BaseRepositoryConstructorTest")]
        public void BaseRepositoryConstructorTest()
        {
            var repository = new BaseRepository<Person>();
            repository.Should().NotBeNull();
        }

        [Fact(DisplayName = "ListByIdOkTest")]
        public void ListByIdOkTest()
        {
            ListConfigMockData();
            var result = _repository.ListById(x => x.BusinessEntityID == 1);
            result.Count().Should().BeGreaterOrEqualTo(1);
        }

        [Fact(DisplayName = "OrderedListOkTest")]
        public void OrderedListOkTest()
        {
            ListConfigMockData();
            var result = _repository.OrderedListByDateAndSize(x => x.ModifiedDate, 5);
            result.Count().Should().BeGreaterOrEqualTo(5);
        }

        #region Configuration Values
        public void PersonMockList()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Person
            {
                BusinessEntityID = i,
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
        private void ListConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();
            PersonMockList();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
        }

        private void BasicConfigMockData()
        {
            personDbSetMock = new Mock<DbSet<Person>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Person).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Person>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Person>(webContextMock.Object);
        }

        public void webContextMockVerify()
        {
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());

        }
        #endregion

    }
}