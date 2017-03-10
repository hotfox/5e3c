using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using Teflon.SDK.Models;
using System.ComponentModel.Composition;
using System.Data.SqlClient;

namespace Teflon.Modules
{
    public class TestContext:DbContext
    {

        public class TestEntityInitializer: CreateDatabaseIfNotExists<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                Test eol = new Test()
                {
                    Name = "EOL",
                    TestCategory = Test.Category.EOL,
                 };

                Product product = new Product()
                {
                    Name = "1200VE",
                    Tests = new List<Test>(),
                };

                product.Tests.Add(eol);

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public TestContext() : base(ConnectionString)
        {
            Database.SetInitializer<TestContext>(new TestEntityInitializer());
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Product> Products { get; set; }

        static private string ConnectionString = GetConnectionString();
        static private string GetConnectionString()
        {
            var cs = new SqlConnectionStringBuilder();
            cs.DataSource = "CH3ULT5QNWXY1\\SQLEXPRESS";
            cs.InitialCatalog = "Teflon";
            cs.UserID = "sa";
            cs.Password = "Sa123456";
            cs.MultipleActiveResultSets = true;
            cs.PersistSecurityInfo = true;
            return cs.ToString();
        }
    }
}
