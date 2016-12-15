using System;
using System.IO;
using System.Linq;
using System.Web.ModelBinding;
using Spartan.ServerCore.Components;
using Spartan.ServerCore.Components.Common;
using Spartan.ServerCore.Components.Communication;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerCore.Components.Player;
using Spartan.ServerNet45.Components;
using Spartan.ServerNet45.Components.DAL;

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

            if (File.Exists("MediaLibrary.db"))
                File.Delete("MediaLibrary.db");

            var db = new SqliteDal("MediaLibrary.db");



            for (var i = 0; i < 15; i++)
            {
                db.CreateTrack($"Track {i}", "Rocker", "Rock em all", i, "ROCK", 100, $"uri {i}");
            }

            var artists = db.GetArtists(new ArtistRequestParameters());
            var albums = db.GetAlbums(new AlbumRequestParameters());
            var genres = db.GetGenres(new GenreRequestParameters());



            //var service = new PlayerService();

            //service.Start("127.0.0.1", "5515");

            //WebApp.Start<WebApiApplication>("127.0.0.1");

            Console.WriteLine("Server started");

            Console.ReadLine();

            //service.Stop();
        }
    }
}
