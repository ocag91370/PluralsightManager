using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    public class Workaround
    {
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            try
            {
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            }
            catch { }

            //try
            //{
            //    var ensureDLLIsCopied = System.Data.SQLite.FunctionType.Aggregate;
            //}
            //catch { }

            //try
            //{
            //    var ensureDLLIsCopied = System.Data.SQLite.EF6.SQLiteProviderFactory.Instance;
            //}
            //catch { }

            //try
            //{
            //    var ensureDLLIsCopied = System.Data.SQLite.Linq.SQLiteProviderFactory.Instance;
            //}
            //catch { }
        }
    }
}
