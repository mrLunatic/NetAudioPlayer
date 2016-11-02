using NAudio.Wave;
using NetAudioPlayer.AudioPlayerServer.Service;
using NetAudioPlayer.Core.Message;
using NetAudioPlayer.Core.Message.AudioServiceMessage;
using NetAudioPlayer.Core.Model;
using SimpleTCP;

namespace NetAudioPlayer.AudioPlayerServer.Model.State
{
    internal abstract class StateBase
    {

        protected WaveOut WaveOut => Service.WaveOut;

        protected Playlist Playlist => Service.Playlist;

        protected SimpleTcpServer Server => Service.SimpleTcpServer;

        public PlayerService Service { get; private set; }

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
                        ErrorCode.UnsupportedMessageType, 
                        Strings.GetString(@"Error_UnsupportedMessage", message.Lang));
            }            
        }

        public virtual void Init(PlayerService service, StateBase prevState)
        {
            Service = service;
        }


        protected IMessage SwitchState<T>(IMessage message = null) where T : StateBase
        {
            return Service.SwitchState<T>(message);
        }

        protected virtual IMessage HandleNextMessage(NextMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang));
        }

        protected virtual IMessage HandlePauseMessage(PauseMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandlePlayMessage(PlayMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandlePrevMessage(PrevMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandleSeekMessage(SeekMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandleStatusInfoMessage(StatusInfoMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandleStatusMessage(StatusMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }

        protected virtual IMessage HandleStopMessage(StopMessage message)
        {
            return new ResponseMessage(
                        ErrorCode.Unhandled,
                        Strings.GetString(@"Error_NotAvliableForCurrentState", message.Lang)); ;
        }
    }
}
