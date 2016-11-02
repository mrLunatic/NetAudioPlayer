using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAudioPlayer.AudioPlayerServer.Service
{


    public class Strings
    {


        static readonly IDictionary<string, string> Ru = new Dictionary<string, string>()
        {
            { @"Error_UnsupportedMessage", @"Данный тип сообщений не поддерживается" },
            { @"Error_NotAvliableForCurrentState", @"Комманда не поддерживается для текущего состояния" }
        }; 


        public static string GetString(string name, string lang = null)
        {
            return Ru[name];
        }
    }
}
