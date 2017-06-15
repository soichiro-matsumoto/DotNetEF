using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database.Config
{
    /// <summary>
    /// AbstractContextのコンフィグクラス
    /// </summary>
    public class ContextConfig
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContextConfig()
        {
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 接続文字列
        /// </summary>
        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// EntityFrameworkから生成されたSQLログ
        /// </summary>
        public virtual Action<string> FuncDatabaseLog { get; set; } = (log) => { };
        #endregion
    }
}
