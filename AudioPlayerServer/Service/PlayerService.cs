using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NetAudioPlayer.AudioPlayerServer.Model;
using NetAudioPlayer.AudioPlayerServer.Model.State;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Model;
using SimpleTCP;

namespace NetAudioPlayer.AudioPlayerServer.Service
{
    public sealed class PlayerService
    {
        internal UniversalItemLoader Loader { get; } = new UniversalItemLoader();

        internal  SimpleTcpServer SimpleTcpServer { get; } = new SimpleTcpServer();

        internal Playlist Playlist { get; set; }

        internal WaveOut WaveOut { get; set; }


        internal StateBase State { get; private set; }

        internal IMessage SwitchState<T>(IMessage message = null) where T : StateBase
        {
            var state = Activator.CreateInstance<T>();

            state.Init(this, State);

            State = state;

            if (message != null)
            {
                return State.HandleMessage(message);
            }

            return null;
        }



        public PlayerService()
        {
            SimpleTcpServer.DataReceived += SimpleTcpServerOnDataReceived;
        }

        private void SimpleTcpServerOnDataReceived(object sender, Message message)
        {
            if (string.IsNullOrEmpty(message.MessageString))
            {
                return;
            }

            var msg = MessageParser.Parse(message.MessageString);

            var response = msg != null 
                ? State.HandleMessage(msg) 
                : new ResponseMessage(
                    ErrorCode.UnknownMessageType,
                    Strings.GetString(@"Error_UnknownMessageType"));

            message.Reply(MessageParser.Serialize(response));
        }

        
    }
}
