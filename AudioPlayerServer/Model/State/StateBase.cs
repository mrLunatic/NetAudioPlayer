using System;
using System.Xml;
using NAudio.Wave;
using NetAudioPlayer.AudioPlayerServer.Components;
using NetAudioPlayer.AudioPlayerServer.Service;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;
using SimpleTCP;
using Strings = NetAudioPlayer.AudioPlayerServer.Components.Strings;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{

    abstract class StateBase
    {
        protected ICommunicationServer Server => ServiceLocator.Current.GetInstance<ICommunicationServer>();

        protected IItemLoader ItemLoader => ServiceLocator.Current.GetInstance<IItemLoader>();

        protected IAudioEngine AudioEngine => ServiceLocator.Current.GetInstance<IAudioEngine>();

        protected IPlaylist Playlist => ServiceLocator.Current.GetInstance<IPlaylist>();

        protected ILocalizationService Localization => ServiceLocator.Current.GetInstance<ILocalizationService>();


        protected abstract PlayerState State { get; }

        public event EventHandler<StateBase> SwitchRequest; 


        protected void SwitchState(StateBase newState)
        {
            SwitchRequest?.Invoke(this, newState);

            newState.OnSwitched(this);
        }

        protected IMessage SwitchState(StateBase newState, IMessage request)
        {
            SwitchRequest?.Invoke(this, newState);

            newState.OnSwitched(this);

            return request != null
                ? newState.HandleMessage(request)
                : null;
        }

        protected virtual void OnSwitched(StateBase prevState)
        {
        }

        public IMessage HandleMessage(IMessage message)
        {
            switch (message.Type)
            {
                case nameof(NextMessage):
                    return HandleNextMessage((NextMessage)message);
                case nameof(PauseMessage):
                    return HandlePauseMessage((PauseMessage)message);
                case nameof(PlayMessage):
                    return HandlePlayMessage((PlayMessage)message);
                case nameof(PrevMessage):
                    return HandlePrevMessage((PrevMessage)message);
                case nameof(SeekMessage):
                    return HandleSeekMessage((SeekMessage)message);
                case nameof(StatusInfoMessage):
                    return HandleStatusInfoMessage((StatusInfoMessage)message);
                case nameof(StatusMessage):
                    return HandleStatusMessage((StatusMessage) message);
                case nameof(StopMessage):
                    return HandleStopMessage((StopMessage) message);
                default:
                    return new ResponseMessage(
                        message,
                        ErrorCode.UnsupportedMessageType, 
                        Localization.Localize(Strings.Error_UnsupportedMessage, message.Lang));
            }            
        }

        protected virtual IMessage HandleNextMessage(NextMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandlePauseMessage(PauseMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandlePlayMessage(PlayMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandlePrevMessage(PrevMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandleSeekMessage(SeekMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandleStatusInfoMessage(StatusInfoMessage message)
        {
            message.CurrentItem = Playlist.Item;
            message.CurrentItemDuration = AudioEngine.Duration.TotalSeconds;
            message.CurrentPosition = AudioEngine.Position.TotalSeconds;
            message.Items = Playlist.Items;
            message.Repeat = Playlist.RepeatMode;
            message.Shuffle = Playlist.Shuffle;
            message.State = State;
            message.Volume = AudioEngine.Volume;

            return message;
        }

        protected virtual IMessage HandleStatusMessage(StatusMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandleStopMessage(StopMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected virtual IMessage HandleSwitchOffMessage(SwitchOffMessage message)
        {
            return new ResponseMessage(
                message,
                ErrorCode.Unhandled,
                Localization.Localize(Strings.Error_NotAvliableForCurrentState, message.Lang));
        }

        protected void SendStatusMessage()
        {
            var message = new StatusInfoMessage
            {
                CurrentItem = Playlist.Item,
                CurrentItemDuration = AudioEngine.Duration.TotalSeconds,
                CurrentPosition = AudioEngine.Position.TotalSeconds,
                Items = Playlist.Items,
                Repeat = Playlist.RepeatMode,
                Shuffle = Playlist.Shuffle,
                State = State,
                Volume = AudioEngine.Volume
            };

            Server.SendBroadcast(message);
        }

        protected void SendShortStatusMessage()
        {
            var message = new ShortStatusInfoMessage()
            {   
                TrackId = Playlist.Item.Id,
                
                CurrentPosition = AudioEngine.Position.TotalSeconds,
            };

            Server.SendBroadcast(message);
        }
    }
}
