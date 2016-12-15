using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartan.Common.Model
{
    public struct SortingItem<T>
    {
        public T Field { get; set; }

        public bool Descending { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public SortingItem(T field, bool descending = false)
        {
            Field = field;
            Descending = descending;
        }
    }
}
