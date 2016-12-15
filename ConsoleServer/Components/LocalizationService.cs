using Spartan.ServerCore.Components.Common;

namespace Spartan.ServerNet45.Components
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
