using System.Collections.Generic;
using System.Data;
using Dapper;
using MtM.Models;

namespace MtM.Repositories
{
   public class JobsRepository
   {
      private readonly IDbConnection _db;

      public JobsRepository(IDbConnection db)
      {
         _db = db;
      }

      internal IEnumerable<Job> GetAll()
      {
         string sql = "SELECT * FROM job;";
         return _db.Query<Job>(sql);
      }

      internal Job GetById(int id)
      {
         string sql = "SELECT * FROM jobs WHERE id = @id;";
         return _db.QueryFirstOrDefault<Job>(sql, new { id });
      }

      internal Job Create(Job newJob)
      {
         string sql = @"
         INSERT INTO job
         (name, amount)
         VALUES
         (@name, @amount);";
         int id = _db.ExecuteScalar<int>(sql, newJob);
         newJob.Id = id;
         return newJob;
      }

      internal Job Edit(Job data)
      {
         string sql = @"
         UPDATE job
         SET
            name = @name,
            amount = @amount,
         WHERE id = @id;
         SELECT * FROM jobs WHERE id = @id;";
         Job returnJob = _db.QueryFirstOrDefault<Job>(sql, data);
         return returnJob;
      }

      internal void Delete(int id)
      {
         string sql = "DELETE FROM jobs WHERE Id = @id LIMIT 1";
         _db.Execute(sql, new { id });
      }
   }
}