using System;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using NetAudioPlayer.ConsoleServer.Components;
using NetAudioPlayer.ConsoleServer.Components.DAL;
using NetAudioPlayer.Core.Components;
using NetAudioPlayer.Core.Components.Common;
using NetAudioPlayer.Core.Components.Communication;
using NetAudioPlayer.Core.Components.DAL;
using NetAudioPlayer.Core.Components.Player;
using NetAudioPlayer.Core.Service;
using NetAudioPlayer.WebApi;
using Owin;

namespace NetAudioPlayer.ConsoleServer
{

    class Program
    {
        static void Main(string[] args)
        {
            ServiceLocator.Current.Register<ICommunicationServer>(() => new TcpServer());
            ServiceLocator.Current.Register<IAudioEngine>(() => new NAudioEngine());
            ServiceLocator.Current.Register<IPlaylist>(() => new Playlist());
            ServiceLocator.Current.Register<ILocalizationService>(() => new LocalizationService());
            ServiceLocator.Current.Register<ITimer>(() => new Timer());

            var db = new SqliteDal("MediaLibrary.db");

            var genre = db.GetGenre(4);

            var service = new PlayerService();

            service.Start("127.0.0.1", "5515");

            //WebApp.Start<WebApiApplication>("127.0.0.1");

            Console.WriteLine("Server started");

            Console.ReadLine();

            service.Stop();
        }
    }
}
