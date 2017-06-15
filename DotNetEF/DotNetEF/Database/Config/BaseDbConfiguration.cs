using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database.Config
{
    /// <summary>
    /// DbConfigurationのスーパークラス
    /// </summary>
    public class BaseDbConfiguration : DbConfiguration
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseDbConfiguration() : base()
        {
        }
        #endregion
    }
}
