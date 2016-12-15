using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Spartan.Common.Data;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerCore.Components.DAL.UpdateParameters;

namespace Spartan.ServerCore.Components.DAL
{
    public interface IDal
    {
        #region Track

        int CreateTrack(string name, string artistName, string albumName, int albumNumber, string genreName, int duration, string uri);
        
        /// <summary>
        /// Возвращает композицию с указанным идентификатором.
        /// <para>Null - если композиция не найдена</para>
        /// </summary>
        /// <param name="id">Идентификатор композиции</param>
        /// <returns></returns>
        ITrack GetTrack(int id);

        /// <summary>
        /// Возвращает список композиций, соответствующих указанным параметрам
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<ITrack> GetTracks(TrackRequestParameters parameters);

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
        int CreateArtist(string name);

        /// <summary>
        /// Возвращает исполнителя с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор исполнителя</param>
        /// <returns>Исполнитель с указанным идентификатором</returns>
        IArtist GetArtist(int id);

        /// <summary>
        /// Возвращает список исполнителей, удовлетворяющих указанным параметрам
        /// </summary>
        /// <param name="parameters">Параметры поиска исполнителей</param>
        /// <returns>Список подходящих исполнителей</returns>
        IEnumerable<IArtist> GetArtists(ArtistRequestParameters parameters);

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

        int CreateAlbum(string name, string artistName, string genreName, int year);

        IAlbum GetAlbum(int id);

        IEnumerable<IAlbum> GetAlbums(AlbumRequestParameters parameters);

        bool UpdateAlbum(int id, AlbumUpdateParameters parameters);

        bool DeleteAlbum(int id);

        #endregion

        #region Genre

        int CreateGenre(string name);

        IGenre GetGenre(int id);

        IEnumerable<IGenre> GetGenres(GenreRequestParameters parameters);

        bool UpdateGenre(int id, GenreUpdateParameters parameters);

        bool DeleteGenre(int id);

        #endregion

        #region Playlist

        int CreatePlaylist(string name);

        IPlaylist GetPlaylist(int id);

        IEnumerable<IPlaylist> GetPlaylists(PlaylistRequestParameters parameters);

        bool UpdatePlaylist(int id, PlaylistUpdateParameters parameters);

        bool DeletePlaylist(int id);

        IEnumerable<int> GetPlaylistTrackIds(int id);

        bool AddTrackToPlaylist(int id, int trackId);

        bool RemoveTrackFromPlaylist(int id, int trackId);

        #endregion   
    }
}
