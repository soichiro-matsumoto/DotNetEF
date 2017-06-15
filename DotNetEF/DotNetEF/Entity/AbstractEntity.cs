using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Entity
{
    /// <summary>
    /// Entity抽象クラス
    /// </summary>
    [DataContract]
    public abstract class AbstractEntity
    {
        /// <summary>
        /// 作成者D
        /// </summary>
        public int CreaterId { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新者ID
        /// </summary>
        public int UpdaterId { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
