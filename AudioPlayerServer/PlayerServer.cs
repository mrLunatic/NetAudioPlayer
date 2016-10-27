using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayerServer.Server;
using NetAudioPlayer.AudioPlayerServer.AudioEngine;

namespace AudioPlayerServer
{
    public sealed class PlayerServer
    {
        private readonly ITcpServer _tcpServer = null;

        private readonly IAudioEngine _audioEngine = null;

        public PlayerServer()
        {            
            _tcpServer.MessageRecieved += OnMessageRecieved;

            _audioEngine.StateChanged += OnAudioEngineStateChanged;
        }

        public void Start(string hostName, int port)
        {
            _tcpServer.Start(hostName, port);            
        }

        public void Stop()
        {
            _tcpServer.Stop();
        }

        private void OnMessageRecieved(ITcpClient client, IMessage message)
        {
            try
            {
                switch (message.MessageType)
                {
                    case nameof(PlayMessage):
                        var playMessage = (PlayMessage) message;
                        _audioEngine.Play(playMessage.ItemPath);
                        client.Send(new ResponseMessage());
                        break;
                    case nameof(PauseMessage):
                        _audioEngine.Pause();
                        client.Send(new ResponseMessage());
                        break;
                    case nameof(StopMessage):
                        _audioEngine.Pause();
                        client.Send(new ResponseMessage());
                        break;
                    case nameof(SeekMessage):
                        var seekMessage = (SeekMessage) message;
                        _audioEngine.Seek(TimeSpan.FromSeconds(seekMessage.Position));
                        client.Send(new ResponseMessage());
                        break;
                    case nameof(StatusMessage):
                        var statusMessage = new StatusMessage
                        {
                            Item = _audioEngine.Item,
                            ItemDuration = _audioEngine.ItemDuration.TotalSeconds,
                            Position = _audioEngine.Position.TotalSeconds,
                            State = _audioEngine.State
                        };
                        client.Send(statusMessage);
                    break;
                    default:
                        client.Send(new ResponseMessage("Unknown message type"));
                        break;                        
                }
            }
            catch (Exception e)
            {
                client.Send(new ResponseMessage(e.Message));
            }
        }

        private void OnAudioEngineStateChanged(object sender, EventArgs eventArgs)
        {
            var statusMessage = new StatusMessage
            {
                Item = _audioEngine.Item,
                ItemDuration = _audioEngine.ItemDuration.TotalSeconds,
                Position = _audioEngine.Position.TotalSeconds,
                State = _audioEngine.State
            };

            _tcpServer.SendToAll(statusMessage);
        }

        private void OnClientConnected(ITcpServer server, ITcpClient tcpClient)
        {
            
        }
    }
}
