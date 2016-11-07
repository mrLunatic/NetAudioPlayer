using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Model;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Общее описание сообщения
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Локаль клиента
        /// </summary>
        string Lang { get; set; }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        Error Error { get; set; }
    }
}
