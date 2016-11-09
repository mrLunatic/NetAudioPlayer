using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Components;
using NetAudioPlayer.Core.Components.Common;

namespace NetAudioPlayer.AudioPlayerServer.Components
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
