using NetAudioPlayer.Core.Attribute;
using NetAudioPlayer.Core.Model;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    /// <summary>
    /// Сообщение-комманда возврата к началу текущего элемента / переходу к предыдущему элементу
    /// </summary>
    [Message]
    public class PrevMessage : MessageBase
    {
    }
}
