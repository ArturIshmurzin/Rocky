using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_DataAccess.Repository.IRepository
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получить элемент
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        T Find(int id);

        /// <summary>
        /// Получить все элементы
        /// </summary>
        /// <param name="filter">выражение фильтрации</param>
        /// <param name="orderBy">способ сортировки</param>
        /// <param name="includeProperties">свойства которые необходимо включить в ответ</param>
        /// <param name="isTracking">включить трасировку запроса</param>
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true
            );

        /// <summary>
        /// Возвращает первый элемент удовлетворяющий условиям или значение по умолчанию
        /// </summary>
        /// <param name="filter">выражение фильтрации</param>
        /// <param name="includeProperties">свойства которые необходимо включить в ответ</param>
        /// <param name="isTracking">включить трасировку запроса</param>
        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true
            );

        /// <summary>
        /// Добавить эдемент
        /// </summary>
        /// <param name="entity">Элемент который необходимо добавить</param>
        void Add(T entity);

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="entity">элемент который необходимо удалить</param>
        void Remove(T entity);

        /// <summary>
        /// Удалить набор элементов
        /// </summary>
        /// <param name="entity">Набор элементов для удаленя</param>
        void RemoveRange(IEnumerable<T> entity);

        /// <summary>
        /// Сохранить изменения в бд
        /// </summary>
        void Save();
    }
}
