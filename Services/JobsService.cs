using System;
using System.Collections.Generic;
using MtM.Models;
using MtM.Repositories;

namespace MtM.Services
{
   public class JobsService
   {
      private readonly JobsRepository _repo;

      public JobsService(JobsRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Job> GetAll()
      {
         return _repo.GetAll();
      }

      internal Job GetById(int id)
      {
         Job data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Job Create(Job newJob)
      {
         return _repo.Create(newJob);
      }

      internal Job Edit(Job updated)
      {
         Job data = GetById(updated.Id);

         data.Name = updated.Name != null ? updated.Name : data.Name;
         data.Amount = updated.Amount != null ? updated.Amount : data.Amount;

         return _repo.Edit(data);
      }

      internal String Delete(int id)
      {
         // NOTE: Why do we declare data? We don't use it...
         Job data = GetById(id);
         _repo.Delete(id);
         return "delorted";
      }
   }
}