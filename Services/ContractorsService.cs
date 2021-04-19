using System;
using System.Collections.Generic;
using MtM.Models;
using MtM.Repositories;

namespace MtM.Services
{
   public class ContractorsService
   {
      private readonly ContractorsRepository _repo;

      public ContractorsService(ContractorsRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Contractor> GetAll()
      {
         return _repo.GetAll();
      }

      internal Contractor GetById(int id)
      {
         Contractor data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Contractor Create(Contractor newContractor)
      {
         return _repo.Create(newContractor);
      }

      internal Contractor Edit(Contractor updated)
      {
         Contractor data = GetById(updated.Id);

         data.Name = updated.Name != null ? updated.Name : data.Name;
         data.Profession = updated.Profession != null ? updated.Profession : data.Profession;

         return _repo.Edit(data);
      }

      internal String Delete(int id)
      {
         // NOTE: Why do we declare data? We don't use it...
         Contractor data = GetById(id);
         _repo.Delete(id);
         return "delorted";
      }
   }
}