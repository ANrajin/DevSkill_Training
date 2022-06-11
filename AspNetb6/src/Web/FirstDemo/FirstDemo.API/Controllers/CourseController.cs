using Autofac;
using FirstDemo.API.Models;
using FirstDemo.Common;
using FirstDemo.Training.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Api.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController, Authorize(Policy = "AccessPermission")]
    [EnableCors("AllowSites")]
    public class CourseController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        public CourseController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        [HttpGet]
        public object Get()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            
            var model = _scope.Resolve<CourseListModel>();
            
            var data = model.GetPagedCourses(dataTablesModel);
            return data;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("GetData")]
        public object GetData()
        {
            var model = _scope.Resolve<CourseListModel>();
            return model.GetCourses();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
