using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ADO_Project.Data
{
    public class ADO_ProjectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ADO_ProjectContext() : base("name=ADO_ProjectContext") //yalnızca yapılandırma için, o yüzden içi boş.
        {
        }

        public System.Data.Entity.DbSet<ADO_Project.Models.Product> Products { get; set; }
    }
}
