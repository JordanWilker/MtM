using System;
using Microsoft.AspNetCore.Mvc;
using MtM.Models;
using MtM.Services;

namespace MtM.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class JobsController : ControllerBase
   {
      private readonly JobsService _service;
      public JobsController(JobsService service)
      {
         _service = service;
      }

      [HttpGet]
      public ActionResult<Job> Get()
      {
         try
         {
            return Ok(_service.GetAll());
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpGet("{id}")]
      public ActionResult<Job> GetAll(int id)
      {
         try
         {
            return Ok(_service.GetById(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPost]
      public ActionResult<Job> Create([FromBody] Job newJob)
      {
         try
         {
            return Ok(_service.Create(newJob));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPut("{id}")]
      public ActionResult<Job> EditJob([FromBody] Job updated, int id)
      {
         try
         {
            updated.Id = id;
            return Ok(_service.Edit(updated));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpDelete("{id}")]
      public ActionResult<Job> Delete(int id)
      {
         try
         {
            return Ok(_service.Delete(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
   }
}