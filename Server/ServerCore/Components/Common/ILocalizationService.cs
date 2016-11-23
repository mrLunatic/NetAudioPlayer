// ReSharper disable InconsistentNaming

namespace NetAudioPlayer.ServerCore.Components.Common
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
