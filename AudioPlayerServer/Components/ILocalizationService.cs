using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace NetAudioPlayer.AudioPlayerServer.Components
{

    public enum Strings
    {
        /// <summary>
        /// Данный тип сообщений не поддерживается
        /// </summary>
        Error_UnsupportedMessage,

        /// <summary>
        /// Комманда не поддерживается для текущего состояния
        /// </summary>
        Error_NotAvliableForCurrentState


    }

    public interface ILocalizationService
    {
        string Localize(Strings str, string lang);
    }
}
