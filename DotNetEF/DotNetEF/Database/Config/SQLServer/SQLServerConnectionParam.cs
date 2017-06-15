using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database.Config.SQLServer
{
    /// <summary>
    /// SQLServerの接続情報
    /// </summary>
    public class SQLServerConnectionParam : IConnectionParam
    {
        #region コンストラクタ
        #endregion

        #region プロパティ
        /// <summary>
        /// 接続先
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// スキーマ
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// ユーザー
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region メソッド
        /// <summary>
        /// 接続文字列を返す
        /// </summary>
        /// <returns>接続文字列</returns>
        public virtual string Build()
        {
            if (this.IsEmptyProperty())
            {
                throw new NotImplementedException("指定されていないパラメータが存在します。");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Data Source = {0};", this.DataSource));
            sb.Append(string.Format("Initial Catalog = {0};", this.InitialCatalog));
            sb.Append(string.Format("User ID = {0};", this.UserId));
            sb.Append(string.Format("Password = {0};", this.Password));
            return sb.ToString();
        }

        /// <summary>
        /// 設定がされていない接続文字列のパラメータが存在するか
        /// </summary>
        /// <returns>設定検証値</returns>
        public bool IsEmptyProperty()
        {
            return string.IsNullOrEmpty(this.DataSource)
                || string.IsNullOrEmpty(this.InitialCatalog)
                || string.IsNullOrEmpty(this.UserId)
                || string.IsNullOrEmpty(this.Password);
        }
        #endregion
    }
}
