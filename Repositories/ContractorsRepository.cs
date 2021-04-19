using System.Collections.Generic;
using System.Data;
using Dapper;
using MtM.Models;

namespace MtM.Repositories
{
   public class ContractorsRepository
   {
      private readonly IDbConnection _db;

      public ContractorsRepository(IDbConnection db)
      {
         _db = db;
      }

      internal IEnumerable<Contractor> GetAll()
      {
         string sql = "SELECT * FROM contractor;";
         return _db.Query<Contractor>(sql);
      }

      internal Contractor GetById(int id)
      {
         string sql = "SELECT * FROM contractors WHERE id = @id;";
         return _db.QueryFirstOrDefault<Contractor>(sql, new { id });
      }

      internal Contractor Create(Contractor newContractor)
      {
         string sql = @"
         INSERT INTO contractor
         (name, profession)
         VALUES
         (@name, @profession);";
         int id = _db.ExecuteScalar<int>(sql, newContractor);
         newContractor.Id = id;
         return newContractor;
      }

      internal Contractor Edit(Contractor data)
      {
         string sql = @"
         UPDATE contractor
         SET
            name = @name,
            profession = @profession,
         WHERE id = @id;
         SELECT * FROM contractors WHERE id = @id;";
         Contractor returnContractor = _db.QueryFirstOrDefault<Contractor>(sql, data);
         return returnContractor;
      }

      internal void Delete(int id)
      {
         string sql = "DELETE FROM contractors WHERE Id = @id LIMIT 1";
         _db.Execute(sql, new { id });
      }
   }
}