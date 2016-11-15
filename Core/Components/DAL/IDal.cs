using System.Collections.Generic;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.Core.Components.DAL
{
    public interface IDal
    {
        #region Track

        /// <summary>
        /// Создает в базе новую композицию
        /// </summary>
        /// <param name="name">Название композиции</param>
        /// <param name="artistId">Идентификатор исполнителя композиции</param>
        /// <param name="albumId">Идентификатор альбома композиции</param>
        /// <param name="albumNumber">Номер композиции в альбоме</param>
        /// <param name="genreId">Идентификатор жанра</param>
        /// <param name="duration">Длительность композиции (сек.)</param>
        /// <param name="uri">Адрес композиции</param>
        /// <param name="tag">Дополнительная метка</param>
        /// <returns>идентификатор созданный композиции</returns>
        int CreateTrack(string name, int artistId, int albumId, int albumNumber, int genreId, int duration, string uri, string tag);

        /// <summary>
        /// Возвращает композицию с указанным идентификатором.
        /// <para>Null - если композиция не найдена</para>
        /// </summary>
        /// <param name="id">Идентификатор композиции</param>
        /// <returns></returns>
        Track GetTrack(int id);

        /// <summary>
        /// Возвращает список композиций, соответствующих указанным параметрам
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<Track> GetTracks(TrackRequestParameters parameters);

        /// <summary>
        /// Обновляет указанные поля у композиции
        /// </summary>
        /// <param name="id">Идентификатор обновляемой композиции</param>
        /// <param name="parameters">Параметры запроса обновления композиции</param>
        /// <returns>True, если композиция была успешно обновлена</returns>
        bool UpdateTrack(int id, TrackUpdateParameters parameters);

        /// <summary>
        /// Удаляет композицию с указанным идентификатором
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True, если композиция была успешно удалена</returns>
        bool DeleteTrack(int id);

        #endregion

        #region Artist

        /// <summary>
        /// Создает исполнителя с указанными параметрами
        /// </summary>
        /// <param name="name">имя исполнителя</param>
        /// <param name="tag">Дополнительная метка</param>
        /// <returns>Идентификатор созанного исполнителя</returns>
        int CreateArtist(string name, string tag);

        /// <summary>
        /// Возвращает исполнителя с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор исполнителя</param>
        /// <returns>Исполнитель с указанным идентификатором</returns>
        Artist GetArtist(int id);

        /// <summary>
        /// Возвращает список исполнителей, удовлетворяющих указанным параметрам
        /// </summary>
        /// <param name="parameters">Параметры поиска исполнителей</param>
        /// <returns>Список подходящих исполнителей</returns>
        IEnumerable<Artist> GetArtists(ArtistRequestParameters parameters);

        /// <summary>
        /// Обновляет исполнителя с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор исполнителя</param>
        /// <param name="parameters">Параметры обновления</param>
        /// <returns>True, если исполнитель был успешно обновлен</returns>
        bool UpdateArtist(int id, ArtistUpdateParameters parameters);

        bool DeleteArtist(int id);

        #endregion

        #region Album

        int CreateAlbum(string name, int artistId, int year, string tag);

        Album GetAlbum(int id);

        IEnumerable<Album> GetAlbums(AlbumRequestParameters parameters);

        bool UpdateAlbum(int id, AlbumUpdateParameters parameters);

        bool DeleteAlbum(int id);

        #endregion

        #region Genres

        int CreateGenre(string name, string tag);

        Genre GetGenre(int id);

        IEnumerable<Genre> GetGenres(GenreRequestParameters parameters);

        bool UpdateGenre(int id, GenreUpdateParameters parameters);

        bool DeleteGenre(int id);

        #endregion

    }
}
