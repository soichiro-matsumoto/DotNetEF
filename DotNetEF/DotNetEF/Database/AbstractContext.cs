using DotNetEF.Database.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Database
{
    /// <summary>
    /// Contextクラスの抽象クラス
    /// </summary>
    public abstract class AbstractContext : DbContext
    {
        /// <summary>
        /// コネクションが解放済みか
        /// </summary>
        private bool _isDisposed = false;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected AbstractContext() : base()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="nameOrConnectionString">接続文字列</param>
        protected AbstractContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config">Contextのコンフィグクラス</param>
        protected AbstractContext(ContextConfig config) : base(config.ConnectionString)
        {
            this.Database.Log = config.FuncDatabaseLog;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AbstractContext"/> class.
        /// </summary>
        ~AbstractContext()
        {
            this.Dispose(true);
        }
        #endregion

        /// <summary>
        /// コネクションが解放済みか
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this._isDisposed;
            }
        }

        #region メソッド
        /// <summary>
        /// DBにコミットする
        /// </summary>
        /// <param name="uid">更新者のユーザーID</param>
        public int SaveChanges(int uid)
        {
            this.SetDefaultColumns(uid);
            return base.SaveChanges();
        }

        /// <summary>
        /// 作成Id、作成日時、更新Id、更新日時
        /// </summary>
        public void SetDefaultColumns(int uid)
        {
            var now = DateTime.Now;
            this.SetCreateColumns(uid, now);
            this.SetUpdateColumns(uid, now);
        }

        /// <summary>
        /// 更新Id、更新日時、セット
        /// </summary>
        public void SetUpdateColumns(int uid, DateTime now)
        {
            var entities = this.ChangeTracker.Entries()
                           .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) // 追加または更新
                               && e.CurrentValues.PropertyNames.Contains("UpdateDate")
                               && e.CurrentValues.PropertyNames.Contains("UpdaterId"))
                           .Select(e => e.Entity);

            foreach (dynamic entity in entities)
            {
                entity.UpdaterId = uid;
                entity.UpdateDate = now;
            }
        }

        /// <summary>
        /// 登録Id、登録日時、セット
        /// </summary>
        public void SetCreateColumns(int uid, DateTime now)
        {
            var entities = this.ChangeTracker.Entries()
                           .Where(e => e.State == EntityState.Added // 追加である
                                && e.CurrentValues.PropertyNames.Contains("CreateDate")
                                && e.CurrentValues.PropertyNames.Contains("CreaterId"))
                           .Select(e => e.Entity);

            foreach (dynamic entity in entities)
            {
                entity.CreaterId = uid;
                entity.CreateDate = now;
            }
        }

        /// <summary>
        /// キャッシュ上のEntityをリロードする（キャッシュを破棄し再取得する）
        /// </summary>
        public void Reload()
        {
            this.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(entity =>
                    {
                        entity.Reload();
                    });
        }

        /// <summary>
        /// キャッシュ上のEntityをリロードする（キャッシュを破棄し再取得する）
        /// </summary>
        /// <typeparam name="TEntity">Contextに存在するEntity</typeparam>
        public void Reload<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Entry<TEntity>(entity).Reload();
        }

        /// <summary>
        /// コネクションを解放する
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            this._isDisposed = disposing;

            base.Dispose(disposing);
        }
        #endregion
    }
}
