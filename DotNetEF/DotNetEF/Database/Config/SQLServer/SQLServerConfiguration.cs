using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database.Config.SQLServer
{
    /// <summary>
    /// SQLServerのDbConfiguration
    /// </summary>
    public class SQLServerConfiguration : BaseDbConfiguration
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SQLServerConfiguration() : base()
        {
            this.SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
            this.SetDefaultConnectionFactory(new SqlConnectionFactory());
        }
        #endregion
    }
}
