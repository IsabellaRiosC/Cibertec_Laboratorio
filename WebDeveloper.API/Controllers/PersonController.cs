using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;

using WebDeveloper.Model;
using WebDeveloper.Repository;
using WebDeveloper.API.Models;

namespace WebDeveloper.API.Controllers
{
    [RoutePrefix("person")]
    public class PersonController : BaseApiController<Person>
    {
       
        public  PersonController(IRepository<Person> repository)
            :base(repository)
        {
              
        }

        [HttpGet]
        [Route("list/{page:int}/{size:int}")]
        public IHttpActionResult List(int? page , int? size) {
            if (!page.HasValue || !size.HasValue)
            {
                page = 1;
                size = 10;
            }
            return Ok(_repository
                .PaginatedList(p=>p.ModifiedDate,page.Value,size.Value));

        } /* 3 UNITES  +  1 UNITEST Que devuelven vacio */

        [HttpGet]
        [Route("totalrows")]
        public IHttpActionResult Rows()
        {
           
            return Ok(_repository
                .GetList().Count);

        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            {

                person.rowguid = Guid.NewGuid();
                person.ModifiedDate = DateTime.Now;
                person.BusinessEntity = new BusinessEntity
                {
                    rowguid = person.rowguid,
                    ModifiedDate = person.ModifiedDate
                };
                _repository.Add(person);
                return Ok();

            }

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Update(Person person)
        {
            if (person == null) return BadRequest();
           
            _repository.Update(person);
            return Ok();

        }

        [HttpDelete]
         [Route("{id:int}")]
        public IHttpActionResult Delete(int? id)
        {
            
           var person = _repository.GetById(x => x.BusinessEntityID==id);
            _repository.Delete(person);
            return Ok();

        }
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Details(int id)
        {

            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return BadRequest();
            
            return Ok(person);


        }

        [HttpGet]
        [Route("type")]
        public IHttpActionResult PersonType()
        {

            var list = new[]
                 {
                    new SelectItem {Value="SC",Text= "Store Contact", Selected=false},
                    new SelectItem {Value="IN",Text= "Individual (retail) customer", Selected=false },
                    new SelectItem {Value="SP",Text= "Sales person" , Selected=false},
                    new SelectItem {Value="EM",Text= "Employee (non-sales)", Selected=false },
                    new SelectItem {Value="VC",Text= "Vendor contact" , Selected=false},
                    new SelectItem {Value="GC",Text= "General contact", Selected=false }
                };
            return Ok(list);
        }

        [HttpGet]
        [Route("email")]
        public IHttpActionResult EmailPromotion()
        {

            var list = new[]
                {
                    new SelectItem {Value="0",Text= "No promotions.", Selected=false},
                    new SelectItem {Value="1",Text= "Promotion Email", Selected=false },
                    new SelectItem {Value="2",Text= "Promotion Email and Partner Email" , Selected=false}

                };

            return Ok(list);

        }


        }
}