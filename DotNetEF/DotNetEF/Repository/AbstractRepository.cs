using DotNetEF.Database;
using DotNetEF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEF.Repository
{
    /// <summary>
    /// Repositoryの抽象クラス
    /// </summary>
    /// <typeparam name="TContext">DbContextを継承したContext</typeparam>
    /// <typeparam name="TEntity">EntityのClass</typeparam>
    public abstract class AbstractRepository<TContext, TEntity>
        where TContext : AbstractContext
        where TEntity : AbstractEntity
    {
        #region フィールド
        /// <summary>
        /// TContext
        /// </summary>
        private readonly TContext _context;

        /// <summary>
        /// TEntityのDbSet
        /// </summary>
        private readonly DbSet<TEntity> _set;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        /// <param name="context">Contextクラス</param>
        protected AbstractRepository(TContext context)
        {
            this._context = context;
            this._set = this._context.Set<TEntity>();
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// TContext 
        /// </summary>
        protected TContext Context
        {
            get { return this._context as TContext; }
        }

        /// <summary>
        /// TEntity のDbSet
        /// </summary>
        protected DbSet<TEntity> Set
        {
            get { return this._set; }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// Queryを取得する
        /// </summary>
        public virtual IQueryable<TEntity> GetQuery()
        {
            return this._set.AsQueryable();
        }

        /// <summary>
        /// Queryを取得する
        /// </summary>
        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> func)
        {
            return this._set.Where(func).AsQueryable();
        }

        /// <summary>
        /// 検索する
        /// </summary>
        public virtual IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> func)
        {
            return this._set.Where(func).ToList();
        }

        /// <summary>
        /// funcの条件で抽出し、orderbyで”昇順”に並び替え、start件目からcount件だけ取得する
        /// </summary>
        /// <typeparam name="TKey">並び替えKey</typeparam>
        /// <param name="func">抽出条件</param>
        /// <param name="orderby">昇順並び替え条件</param>
        /// <param name="start">StartするIndex</param>
        /// <param name="count">抽出件数</param>
        /// <returns>TEntityのコレクション</returns>
        public virtual IEnumerable<TEntity> LimitOrderBy<TKey>(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, TKey>> orderby, int start, int count)
        {
            return this._set.Where(func)
                            .OrderBy(orderby)
                            .Skip(start - 1)
                            .Take(count)
                            .ToList();
        }

        /// <summary>
        /// funcの条件で抽出し、orderbyで”降順”に並び替え、start件目からcount件だけ取得する
        /// </summary>
        /// <typeparam name="TKey">並び替えKey</typeparam>
        /// <param name="func">抽出条件</param>
        /// <param name="orderbydesc">降順並び替え条件</param>
        /// <param name="start">StartするIndex</param>
        /// <param name="count">抽出件数</param>
        /// <returns>TEntityのコレクション</returns>
        public virtual IEnumerable<TEntity> LimitOrderByDescending<TKey>(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, TKey>> orderbydesc, int start, int count)
        {
            return this._set.Where(func)
                            .OrderByDescending(orderbydesc)
                            .Skip(start - 1)
                            .Take(count)
                            .ToList();
        }

        /// <summary>
        /// 全件取得する
        /// </summary>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this._set.ToList();
        }

        /// <summary>
        /// 主キーで検索
        /// </summary>
        public virtual TEntity Find(params object[] param)
        {
            return this._set.Find(param);
        }

        /// <summary>
        /// 最初の1件を取得する
        /// </summary>
        public virtual TEntity First(Expression<Func<TEntity, bool>> func)
        {
            return this._set.First(func);
        }

        /// <summary>
        /// 最初の1件を取得する
        /// </summary>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> func)
        {
            return this._set.FirstOrDefault(func);
        }

        /// <summary>
        /// 1件を取得する
        /// </summary>
        public virtual TEntity SingleEntity(Expression<Func<TEntity, bool>> func)
        {
            return this._set.Single(func);
        }

        /// <summary>
        /// 1件を取得する
        /// </summary>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> func)
        {
            return this._set.SingleOrDefault(func);
        }

        /// <summary>
        /// 存在チェック。
        /// 存在するならtrueを返す。
        /// </summary>
        public virtual bool Any(Expression<Func<TEntity, bool>> func)
        {
            return this._set.Any(func);
        }

        /// <summary>
        /// 件数を返す
        /// </summary>
        public virtual int Count(Expression<Func<TEntity, bool>> func)
        {
            return this._set.Count(func);
        }

        /// <summary>
        /// 新しいEntityを作成する
        /// </summary>
        public virtual TEntity Create()
        {
            return this._set.Create();
        }

        /// <summary>
        /// 追加する
        /// </summary>
        public virtual void Add(TEntity entity)
        {
            this._set.Add(entity);
        }

        /// <summary>
        /// 追加する
        /// </summary>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            this._set.AddRange(entities);
        }

        /// <summary>
        /// 削除する
        /// </summary>
        public virtual void Remove(TEntity entity)
        {
            this._set.Remove(entity);
        }

        /// <summary>
        /// 削除する
        /// </summary>
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            this._set.RemoveRange(entities);
        }
        #endregion
    }
}
