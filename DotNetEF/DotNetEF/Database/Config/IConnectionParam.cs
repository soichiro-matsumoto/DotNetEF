using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database.Config
{
    /// <summary>
    /// 接続情報クラスのインターフェイス
    /// </summary>
    public interface IConnectionParam
    {
        /// <summary>
        /// 接続文字列を返す
        /// </summary>
        /// <returns>接続文字列</returns>
        string Build();
    }
}
