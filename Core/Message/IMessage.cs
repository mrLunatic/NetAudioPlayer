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
        /// Тип сообщения
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        Error Error { get; set; }
    }
}
