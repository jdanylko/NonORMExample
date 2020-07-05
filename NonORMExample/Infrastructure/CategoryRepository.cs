using System.Collections.Generic;
using System.Data.SqlClient;
using NonORMExample.Models;

namespace NonORMExample.Infrastructure
{
    public class CategoryRepository : AdoRepository<Category>
    {
        public CategoryRepository(string connectionString)
            : base(connectionString)
        {
        }
        
        public void Add(Category category)
        {
            var builder = new SqlQueryBuilder<Category>(category);
            ExecuteCommand(builder.GetInsertCommand());
        }
        
        public void Update(Category category)
        {
            var builder = new SqlQueryBuilder<Category>(category);
            ExecuteCommand(builder.GetUpdateCommand());
        }
        
        public void Delete(Category category)
        {
            var builder = new SqlQueryBuilder<Category>(category);
            ExecuteCommand(builder.GetDeleteCommand());
        }
        
        #region Get
        
        public IEnumerable<Category> GetAll()
        {
            // DBAs across the country are having strokes 
            //  over this next command!
            using var command = new SqlCommand("SELECT * FROM Categories");
            return GetRecords(command);
        }
        
        public Category GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using var command = new SqlCommand("SELECT * FROM Categories WHERE CategoryID = @id");
            command.Parameters.Add(new SqlParameter("id", id));
            return GetRecord(command);
        }
        
        #endregion
        
        public override Category PopulateRecord(SqlDataReader reader)
        {
            return new Category
            {
                CategoryId = reader.GetInt32(0),
                CategoryName = reader.GetString(1),
                Description = reader.GetString(2)
            };
        }
    }
}