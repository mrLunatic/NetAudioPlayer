using System.Collections.Generic;
using Spartan.Common.Data;

namespace Spartan.ServerCore.Components.DAL.UpdateParameters
{
    /// <summary>
    /// Общие параметры обновления сущности
    /// </summary>
    public abstract class CommonUpdateParameters
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Дополнительная метка
        /// </summary>
        public string Tag { get; set; }

    }
}