using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAudioPlayer.AudioPlayerServer.Components;
using NetAudioPlayer.AudioPlayerServer.Model;
using NetAudioPlayer.Core.Model;

namespace AudioPlayerServerTests
{
    [TestClass]
    public class PlayListTest
    {
        class ItemLoader : IItemLoader
        {
            public PlaylistItem LoadItem(string item)
            {
                return new PlaylistItem(item, null);
            }

            /// <summary>
            /// Удаляет из памяти все предыдущие компоненты
            /// </summary>
            public void Reset()
            {
            }

            #region Implementation of IDisposable

            /// <summary>
            /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
            /// </summary>
            public void Dispose()
            {
            }

            #endregion
        }

        private readonly string[] _items = new[] { "1", "2", "3", "4" };

        [TestMethod]
        public void Base()
        {
            var playList = new Playlist(new ItemLoader());

            playList.Init(_items);

            Assert.IsNull(playList.Item, @"При инициализации Item не должен быть указан!");

            CollectionAssert.AreEqual(_items, playList.Items.ToArray(), @"Список элементов составлен неверно");

            playList.Reset();

            Assert.IsNull(playList.Item, @"После сброса Item не должен быть указан!");       
        }

        [TestMethod]
        public void NoRepeatMode()
        {
            var playList = new Playlist(new ItemLoader());

            playList.Init(_items);

            playList.Play(_items[0]);

            foreach (var item in _items)
            {
                Assert.IsNotNull(playList.Item, "Не указан воспроизводимый трек");
                Assert.AreEqual(item, playList.Item.Name, @"Воспроизводится неправильный трек");
                playList.Next();
            }

            Assert.IsNull(playList.Item, "Плейлист не закончил воспроизведение");

            playList.Shuffle = true;
            playList.Init(_items);

            var played = new List<string>();

            for (var i = 0; i < _items.Length; i++)
            {
                playList.Next();
                Assert.IsFalse(played.Contains(playList.Item.Name), "Повторение элемента в режиме NoRepeat");
                played.Add(playList.Item.Name);
            }

            played.Reverse();

            for (var i = 0; i < _items.Length; i++)
            {
                Assert.AreEqual(played[i], playList.Item.Name, "Ошибка перехода к предыдущему треку");
                playList.Prev();
            }

        }

        [TestMethod]
        public void RepeatOneMode()
        {
            var playList = new Playlist(new ItemLoader())
            {
                RepeatMode = RepeatMode.One
            };

            playList.Init(_items);

            playList.Play(_items[0]);

            for (var i  = 0; i < _items.Length; i++)
            {                
                Assert.IsNotNull(playList.Item, "Не указан воспроизводимый трек");
                Assert.AreEqual(_items[0], playList.Item.Name, @"Воспроизводится неправильный трек");
                playList.Next();
            }

            playList.Shuffle = true;

            for (var i = 0; i < _items.Length; i++)
            {
                Assert.IsNotNull(playList.Item, "Не указан воспроизводимый трек");
                Assert.AreEqual(_items[0], playList.Item.Name, @"Воспроизводится неправильный трек");
                playList.Next();
            }
        }

        [TestMethod]
        public void RepeatAllMode()
        {
            var playList = new Playlist(new ItemLoader())
            {
                RepeatMode = RepeatMode.All
            };

            playList.Init(_items);

            playList.Play(_items[0]);

            for (var i = 0; i < 2; i++)
            {
                foreach (var item in _items)
                {
                    Assert.IsNotNull(playList.Item, @"Не указан воспроизводимый трек");
                    Assert.AreEqual(item, playList.Item.Name, @"Воспроизводится неправильный трек");
                    playList.Next();
                }
            }

        }

    }
}
