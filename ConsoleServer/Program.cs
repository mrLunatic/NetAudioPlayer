using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.AudioPlayerServer.Components;
using NetAudioPlayer.AudioPlayerServer.Service;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.Current.Register<ICommunicationServer>(() => new TcpServer());
            ServiceLocator.Current.Register<IAudioEngine>(() => new NAudioEngine());
            ServiceLocator.Current.Register<IPlaylist>(() => new Playlist());
            ServiceLocator.Current.Register<ILocalizationService>(() => new LocalizationService());

            var service = new PlayerService();

            service.Start("127.0.0.1", "5515");

            Console.WriteLine("Server started");

            Console.ReadLine();

            service.Stop();
        }
    }
}
