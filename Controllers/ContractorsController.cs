using System;
using Microsoft.AspNetCore.Mvc;
using MtM.Models;
using MtM.Services;

namespace MtM.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ContractorsController : ControllerBase
   {
      private readonly ContractorsService _service;
      public ContractorsController(ContractorsService service)
      {
         _service = service;
      }

      [HttpGet]
      public ActionResult<Contractor> Get()
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
      public ActionResult<Contractor> GetAll(int id)
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
      public ActionResult<Contractor> Create([FromBody] Contractor newContractor)
      {
         try
         {
            return Ok(_service.Create(newContractor));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPut("{id}")]
      public ActionResult<Contractor> EditContractor([FromBody] Contractor updated, int id)
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
      public ActionResult<Contractor> Delete(int id)
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