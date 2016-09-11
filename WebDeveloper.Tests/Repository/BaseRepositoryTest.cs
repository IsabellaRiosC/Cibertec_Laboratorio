using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using Xunit;

namespace WebDeveloper.Tests.Repository
{
    public class BaseRepositoryTest

        // sirve para testear proyecto
    {
        private IRepository<Person> repository;

        public BaseRepositoryTest()
        {
            repository = new BaseRepository<Person>();

        }
        [Fact(DisplayName = "AddTestWrongWithMissingData")]
        public void AddTestWrongWithMissingData()
        {

            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";

            person.rowguid = Guid.NewGuid();
            try
            {
                repository.Add(person);

            }
            catch (Exception exception)
            {
                exception.Source.Should().Be("EntityFramework");

            }

        }

        [Fact (DisplayName ="AddTestWrongWithNull")]

        public void AddTestWrongWithNull() {
            var person = new Person();
           
            try
            {
                repository.Add(person);

            }
            catch (Exception exception)
            {
                exception.Source.Should().NotBeNull();

            }
        }

        [Fact(DisplayName = "AddTestWithProperData")]
        public void AddTestWithProperData()
        {

            var person = new Person();
            person.PersonType = "SC";
            person.FirstName = "Test";
            person.LastName = "Test";
            person.rowguid = Guid.NewGuid();
            person.ModifiedDate = DateTime.Now;
            person.BusinessEntity = new BusinessEntity
            {

                ModifiedDate = person.ModifiedDate,
                rowguid = person.rowguid

            };
            var result = repository.Add(person);
            result.Should().BeGreaterThan(0);

        }
    }
}
