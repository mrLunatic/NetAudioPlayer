using NetAudioPlayer.ServerCore.Components;
using NetAudioPlayer.ServerCore.Components.Communication;
using NetAudioPlayer.ServerCore.Components.Player;
using NetAudioPlayer.ServerCore.Components.State;

namespace NetAudioPlayer.ServerCore.Service
{
    public sealed class PlayerService
    {
        private StateBase _state;

        internal ICommunicationServer Server => ServiceLocator.Current.GetInstance<ICommunicationServer>();

        internal IItemLoader ItemLoader => ServiceLocator.Current.GetInstance<IItemLoader>();

        internal IAudioEngine AudioEngine => ServiceLocator.Current.GetInstance<IAudioEngine>();

        internal IPlaylist Playlist => ServiceLocator.Current.GetInstance<IPlaylist>();

        /// <summary>
        /// Текущее состояние
        /// </summary>
        internal StateBase State
        {
            get { return _state; }
            private set
            {
                if (_state != null)
                {
                    _state.SwitchRequest -= StateOnSwitchRequest;
                }

                _state = value;

                if (_state != null)
                {
                    _state.SwitchRequest += StateOnSwitchRequest;
                }
            }
        }


        public PlayerService()
        {
            Server.RequestRecieved += SimpleTcpServerOnRequestRecieved;

            State = States.Idle;
        }

        public void Start(string host, string service)
        {
            Server.Start(host, service);
        }

        public void Stop()
        {
            Server.Stop();
        }

        private void SimpleTcpServerOnRequestRecieved(object sender, RequestRecievedEventArgs args)
        {
            args.Response = State.HandleMessage(args.Request);
        }

        private void StateOnSwitchRequest(object sender, StateBase state)
        {
            State = state;
        }


    }
}
