using System;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using Newtonsoft.Json;

namespace NetAudioPlayer.Core.Message
{
    public class AudioServiceMessageParser
    {
        public event EventHandler<PlayMessage> PlayMessageRecieved;

        public event EventHandler<PauseMessage> PauseMessageRecieved;

        public event EventHandler<StopMessage> StopMessageRecieved;

        public event EventHandler<NextMessage> NextMessageRecieved;

        public event EventHandler<PrevMessage> PrevMessageRecieved;

        public event EventHandler<SeekMessage> SeekMessageRecieved;

        public event EventHandler<ShortStatusInfoMessage> ShortStatusInfoMessageRecieved;

        public event EventHandler<StatusInfoMessage> StatusInfoRecieved;

        public event EventHandler<StatusMessage> StatusMessageRecieved;

        public event EventHandler<string> UnknownMessageRecieved;

         

        public void Parse(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            var msg = JsonConvert.DeserializeObject<IMessage>(message);

            switch (msg.Type)
            {
                case nameof(PlayMessage):
                    OnPlayMessageRecieved(JsonConvert.DeserializeObject<PlayMessage>(message));
                    break;
                case nameof(PauseMessage):
                    OnPauseMessageRecieved(JsonConvert.DeserializeObject<PauseMessage>(message));
                    break;
                case nameof(StopMessage):
                    OnStopMessageRecieved(JsonConvert.DeserializeObject<StopMessage>(message));
                    break;
                case nameof(NextMessage):
                    OnNextMessageRecieved(JsonConvert.DeserializeObject<NextMessage>(message));
                    break;
                case nameof(PrevMessage):
                    OnPrevMessageRecieved(JsonConvert.DeserializeObject<PrevMessage>(message));
                    break;
                case nameof(SeekMessage):
                    OnSeekMessageRecieved(JsonConvert.DeserializeObject<SeekMessage>(message));
                    break;
                case nameof(ShortStatusInfoMessage):
                    OnShortStatusInfoMessageRecieved(JsonConvert.DeserializeObject<ShortStatusInfoMessage>(message));
                    break;
                case nameof(StatusInfoMessage):
                    OnStatusInfoRecieved(JsonConvert.DeserializeObject<StatusInfoMessage>(message));
                    break;
                case nameof(StatusMessage):
                    OnStatusMessageRecieved(JsonConvert.DeserializeObject<StatusMessage>(message));
                    break;
                default:
                    OnUnknownMessageRecieved(message);
                    break;
            }

        }

        protected virtual void OnPlayMessageRecieved(PlayMessage e)
        {
            PlayMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnPauseMessageRecieved(PauseMessage e)
        {
            PauseMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnStopMessageRecieved(StopMessage e)
        {
            StopMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnNextMessageRecieved(NextMessage e)
        {
            NextMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnPrevMessageRecieved(PrevMessage e)
        {
            PrevMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnSeekMessageRecieved(SeekMessage e)
        {
            SeekMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnShortStatusInfoMessageRecieved(ShortStatusInfoMessage e)
        {
            ShortStatusInfoMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnStatusInfoRecieved(StatusInfoMessage e)
        {
            StatusInfoRecieved?.Invoke(this, e);
        }

        protected virtual void OnStatusMessageRecieved(StatusMessage e)
        {
            StatusMessageRecieved?.Invoke(this, e);
        }

        protected virtual void OnUnknownMessageRecieved(string e)
        {
            UnknownMessageRecieved?.Invoke(this, e);
        }
    }
}
