using System;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAudioPlayer.AudioPlayerServer.Model;

namespace AudioPlayerServerTests
{
    class ItemLoader : IPlayListItemLoader
    {
        public PlayListItem LoadItem(string item)
        {
            return new PlayListItem(item, null);
        }
    }

    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _items = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", };

        [TestMethod]
        public void PlayListTest()
        {            
            var playList = new Playlist(new ItemLoader());

            playList.Init(_items);

            Assert.IsNull(playList.Item, @"При инициализации Item не должен быть указан!");

            CollectionAssert.AreEqual(_items, playList.Items.ToArray(), @"Список элементов составлен неверно");

            playList.Reset();

            Assert.IsNull(playList.Item, @"После сброса Item не должен быть указан!");

            playList.Init(_items);

            playList.Play(_items[0]);

            foreach (string item in _items)
            {
                Assert.IsNotNull(playList.Item, "Не указан воспроизводимый трек");
                Assert.AreEqual(item, playList.Item.Name, @"Воспроизводится неправильный трек");
                playList.Next();
            }

            Assert.IsNull(playList.Item, "Плеер не закончил воспроизведение");
        }
    }
}
