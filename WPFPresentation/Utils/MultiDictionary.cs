using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Utils
{
    public class MultiDictionary<T, K>: Dictionary<T, List<K>>
    {
       
        #region Private Mehthods

        //comprueba si la clave ya esta presente
        private void EnsureKey(T key)
        {
            if (!ContainsKey(key))
            {
                this[key] = new List<K>();
            }
            else
            {
                if (this[key] == null)
                    this[key] = new List<K>();
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Agrega una lista de valores para agregar a la lista de valores
        /// </summary>
        /// <param name="key">La llave para agregar ek item in la lista de valores</param>
        /// <param name="value"></param>
        public void AddValues(T key, K value)
        {
            EnsureKey(key);
            if (!this[key].Contains(value))
                this[key].Add(value);
        }

        /// <summary>
        /// Quita una clave especifica del diccionario
        /// si la lista de valores esta vacia la clave se remueve del diccionario
        /// </summary>
        /// <param name="key">La clave de donde se remueve el valor</param>
        /// <param name="value">El valor a remover</param>
        /// <returns>Devuelve falso si la clave no fue encontrada</returns>
        public bool RemoveValue(T key, K value)
        {
            if (!ContainsKey(key))
                return false;

            this[key].Remove(value);

            if (this[key].Count == 0)
                this.Remove(key);

            return true;
        }

        /// <summary>
        /// Quita una clave especifica del diccionario
        /// si la lista de valores esta vacia la clave se remueve del diccionario
        /// </summary>
        /// <param name="key">La clave de donde se remueve el valor</param>
        /// <param name="match">El predicado para encotrar el item</param>
        /// <returns>Devuelve falso si la clave no fue encontrada</returns>
        public bool RemoveAllValue(T key, Predicate<K> match)
        {
            if (!ContainsKey(key))
                return false;

            this[key].RemoveAll(match);

            if (this[key].Count == 0)
                this.Remove(key);

            return true;
        }
        #endregion
    }
}
