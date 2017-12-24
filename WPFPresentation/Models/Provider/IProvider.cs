using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Models.Provider
{
   
    // provider interface to data Services
    
    public interface IProvider<TEntity> where TEntity: class 
    {
        TEntity Get(int id);
        ObservableCollection<TEntity> GetAll();
        ObservableCollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Devuelve una instancia del tipo evaluado por el predicado o de no existir retorna nullo
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Agrega una nueva entidad del Tipo Expecificado
        /// </summary>
        /// <param name="entity"></param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Agregar una Coleccion de entidades del Tipo Expecificado
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(ObservableCollection<TEntity> entities);

        /// <summary>
        /// Elimina una entidad del Tipo Especificado
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Elimina una coleccion del Tipo Especificado
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(ObservableCollection<TEntity> entities);

        TEntity Update(TEntity entity);
    }
}
