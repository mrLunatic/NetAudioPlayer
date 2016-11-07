using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.AudioPlayerServer.Components;

namespace NetAudioPlayer.AudioPlayerServer.Service
{
    public interface IServiceLocator : IDisposable
    {
        /// <summary>
        /// Возвращает true, если для указанного типа зарегестрирован объект
        /// </summary>
        /// <typeparam name="T">Тип, для которого выполняется проверка</typeparam>
        /// <param name="key">Ключ объекта</param>
        /// <returns></returns>
        bool IsRegistered<T>(string key = null) where T : class;

        /// <summary>
        /// Регистрирует в системе тип, привязывая к неку указанный метод
        /// </summary>
        /// <typeparam name="T">Регистрируемый тип</typeparam>
        /// <param name="factoryAction">Метод-фабрика для объекта</param>
        /// <param name="key">Ключ объекта</param>
        /// <param name="lazy">Ленивая загрузка. 
        /// <para>Указывает, выполнять ли метод сразу, либо при первом доступе</para>
        /// </param>
        void Register<T>(Func<T> factoryAction, string key = null, bool lazy = true) where T : class;

        /// <summary>
        /// Возвращает экземпляр, зарегистрированный для указанного типа
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <param name="key">Ключ объекта</param>
        /// <returns></returns>
        T GetInstance<T>(string key = null) where T : class;

        /// <summary>
        /// Возвращает все экземляры объектов, привязанных к соответствующему типу
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAllInstances<T>() where T : class;

        /// <summary>
        /// Отменяет регистрацию указанного типа
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <param name="key"></param>
        void Unregister<T>(string key = null) where T : class;
    }

    public class ServiceLocator : IServiceLocator
    {
        #region Singleton

        private static ServiceLocator _current;

        public static IServiceLocator Current => _current ?? (_current = new ServiceLocator());

        #endregion

        private class Item<T> : IDisposable where T : class 
        {
            private readonly Func<T> _factory;

            private T _instance;

            public T Instance => _instance ?? (_instance = _factory.Invoke());

            public Item(Func<T> factory, bool lazy = true)
            {
                if (factory == null)
                {
                    throw  new ArgumentNullException(nameof(factory));
                }

                _factory = factory;

                if (!lazy)
                {
                    _instance = factory.Invoke();
                }
            }

            public Item(T instance)
            {
                _instance = instance;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                if (_instance is IDisposable)
                {
                    ((IDisposable)_instance).Dispose();
                }
            }

            #endregion
        }

        private readonly IDictionary<Type, object> _items = new Dictionary<Type, object>();  

       

        #region IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            foreach (var items in _items.Values.OfType<IDictionary>())
            {
                foreach (var item in items.Values.OfType<IDisposable>())
                {
                    item.Dispose();
                }
            }
        }

        #endregion

        #region Implementation of IServiceLocator

        /// <summary>
        /// Возвращает true, если для указанного типа зарегестрирован объект
        /// </summary>
        /// <typeparam name="T">Тип, для которого выполняется проверка</typeparam>
        /// <param name="key">Ключ объекта</param>
        /// <returns></returns>
        public bool IsRegistered<T>(string key = null) where T : class
        {
            var items = GetItems<T>();

            if (items == null)
                return false;

            return key == null
                ? items.Count != 0
                : items.ContainsKey(key);
        }

        /// <summary>
        /// Регистрирует в системе тип, привязывая к неку указанный метод
        /// </summary>
        /// <typeparam name="T">Регистрируемый тип</typeparam>
        /// <param name="factory">Метод-фабрика для объекта</param>
        /// <param name="key">Ключ объекта</param>
        /// <param name="lazy">Ленивая загрузка. 
        /// <para>Указывает, выполнять ли метод сразу, либо при первом доступе</para>
        /// </param>
        public void Register<T>(Func<T> factory, string key = null, bool lazy = true) where T : class 
        {
            var items = GetItems<T>();

            if (items == null)
            {
                items = new Dictionary<string, Item<T>>();

                _items.Add(typeof(T), items);
            }

            items.Add(key ?? string.Empty, new Item<T>(factory, lazy));
        }

        /// <summary>
        /// Возвращает экземпляр, зарегистрированный для указанного типа
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <param name="key">Ключ объекта</param>
        /// <returns></returns>
        public T GetInstance<T>(string key = null) where T : class
        {
            var items = GetItems<T>();

            return items?[key ?? string.Empty]?.Instance;
        }

        /// <summary>
        /// Возвращает все экземляры объектов, привязанных к соответствующему типу
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAllInstances<T>() where T : class
        {
            var items = GetItems<T>();

            return items?.Values.Select(p => p.Instance);
        }

        /// <summary>
        /// Отменяет регистрацию указанного типа
        /// </summary>
        /// <typeparam name="T">Зарегистрированный тип</typeparam>
        /// <param name="key"></param>
        public void Unregister<T>(string key = null) where T : class
        {
            var items = GetItems<T>();

            if (items == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(key))
            {
                Item<T> item;
                if (items.TryGetValue(key, out item))
                {
                    items.Remove(key);
                    item.Dispose();
                }
                if (items.Count == 0)
                {
                    _items.Remove(typeof (T));
                }                
            }
            else
            {
                _items.Remove(typeof(T));

                foreach (var item in items.Values)
                {
                    item.Dispose();
                }

            }

        }

        #endregion

        private IDictionary<string, Item<T>> GetItems<T>() where T : class
        {
            object items;

            if (_items.TryGetValue(typeof (T), out items))
            {
                return items as IDictionary<string, Item<T>>;
            }
            return null;
        }
    }
}
