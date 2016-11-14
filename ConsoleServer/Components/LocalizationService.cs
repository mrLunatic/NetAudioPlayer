using NetAudioPlayer.Core.Components.Common;

namespace NetAudioPlayer.ConsoleServer.Components
{
    public sealed class LocalizationService : ILocalizationService
    {
        #region Implementation of ILocalizationService

        public string Localize(Strings str, string lang)
        {
            return str.ToString();
        }

        #endregion
    }
}
