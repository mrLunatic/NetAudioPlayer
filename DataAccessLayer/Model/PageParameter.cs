using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.DataAccessLayer.Model
{
    /// <summary>
    /// Параметры пагинации
    /// </summary>
    public class PageParameter
    {
        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// Номер запрашиваемой страницы
        /// </summary>
        public int Index { get; set; }
    }
}
