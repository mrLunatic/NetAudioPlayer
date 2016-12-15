using System.CodeDom;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerNet45.Data;

namespace Spartan.ServerNet45.Components.DAL
{
    public partial class SqliteDal
    {

        private static readonly string AlbumInsertTrigger = $@"{Album.Table}_insert_trigger";

        private static readonly string AlbumUpdateTrigger = $@"{Album.Table}_update_trigger";

        private static readonly string AlbumDeleteTrigger = $@"{Album.Table}_delete_trigger";

        private static readonly string TrackInsertTrigger = $@"{Track.Table}_insert_trigger";

        private static readonly string TrackUpdateTrigger = $@"{Track.Table}_update_trigger";

        private static readonly string TrackDeleteTrigger = $@"{Track.Table}_delete_trigger";

        private static readonly string PlaylistMapInsertTrigger = $@"{PlaylistMap.Table}_insert_trigger";

        private static readonly string PlaylistMapUpdateTrigger = $@"{PlaylistMap.Table}_update_trigger";

        private static readonly string PlaylistMapDeleteTrigger = $@"{PlaylistMap.Table}_delete_trigger";


        private void CreateDb()
        {
            CreateGenreTable();
            CreateArtistTable();
            CreateAlbumTable();
            CreateTrackTable();
            CreatePlaylistTable();
            CreateDefaultItems();
        }

        private void CreateGenreTable()
        {
            var cmd = $@"
CREATE TABLE [{Genre.Table}]
(
  [{Genre.IdColumn    }]  INTEGER  NOT NULL  UNIQUE,
  [{Genre.NameColumn  }]  TEXT     NOT NULL  UNIQUE,
  [{Genre.RatingColumn}]  INTEGER  NOT NULL  DEFAULT {Genre.DefaultRating},
  [{Genre.TagColumn   }]  TEXT     NOT NULL  DEFAULT {Genre.DefaultTag.AsSqlString()},

  PRIMARY KEY ([{Genre.IdColumn}])
);";

            ExecuteScalar(cmd);
        }

        private void CreateArtistTable()
        {
            var cmd = $@"
CREATE TABLE [{Artist.Table}]
(
  [{Artist.IdColumn         }]  INTEGER  NOT NULL UNIQUE,
  [{Artist.NameColumn       }]  TEXT     NOT NULL UNIQUE,
  [{Artist.RatingColumn     }]  INTEGER  NOT NULL DEFAULT {Artist.DefaultRating},
  [{Artist.TagColumn        }]  TEXT     NOT NULL DEFAULT {Artist.DefaultTag.AsSqlString()},
  [{Artist.AlbumsCountColumn}]  INTEGER  NOT NULL DEFAULT 0,
  [{Artist.TracksCountColumn}]  INTEGER  NOT NULL DEFAULT 0,

  PRIMARY KEY  ([{Artist.IdColumn}]));";

            ExecuteScalar(cmd);
        }

        private void CreateAlbumTable()
        {
            var cmd = $@"
CREATE TABLE [{Album.Table}] (
  [{Album.IdColumn          }]  INTEGER  NOT NULL  UNIQUE,
  [{Album.NameColumn        }]  TEXT     NOT NULL  UNIQUE,
  [{Album.RatingColumn      }]  INTEGER  NOT NULL  DEFAULT {Album.DefaultRating}, 
  [{Album.TagColumn         }]  TEXT     NOT NULL  DEFAULT {Album.DefaultTag.AsSqlString()},
  [{Album.ArtistIdColumn    }]  INTEGER  NOT NULL  DEFAULT {Artist.DefaultId},
  [{Album.GenreIdColumn     }]  INTEGER  NOT NULL  DEFAULT {Genre.DefaultId},
  [{Album.YearColumn        }]  INTEGER  NOT NULL  DEFAULT {Album.DefaultYear},
  [{Album.TracksCountColumn }]  INTEGER  NOT NULL  DEFAULT 0, 

  PRIMARY KEY ([{Album.IdColumn}]),

  UNIQUE ([{Album.NameColumn}], [{Album.ArtistIdColumn}]),

  FOREIGN KEY ([{Album.ArtistIdColumn}])
    REFERENCES [{Artist.Table}]([{Artist.IdColumn}])
    ON DELETE SET DEFAULT,

  FOREIGN KEY ([{Album.GenreIdColumn }])
    REFERENCES [{Genre.Table }]([{Genre.IdColumn }])
    ON DELETE SET DEFAULT);

CREATE TRIGGER [{AlbumInsertTrigger}] 
  AFTER INSERT ON [{Album.Table}]
BEGIN
  UPDATE {Artist.Table} 
    SET {Artist.AlbumsCountColumn} = {Artist.AlbumsCountColumn} + 1
    WHERE {Artist.IdColumn} = {New}.{Album.ArtistIdColumn};
END;

CREATE TRIGGER [{AlbumUpdateTrigger}] 
  AFTER UPDATE ON [{Album.Table}]
  WHEN {New}.{Album.ArtistIdColumn} != {Old}.{Album.ArtistIdColumn}
BEGIN 
  UPDATE {Artist.Table}
    SET {Artist.AlbumsCountColumn} = {Artist.AlbumsCountColumn} + 1
    WHERE {Artist.IdColumn} = {New}.{Album.ArtistIdColumn};
  UPDATE {Artist.Table}
    SET {Artist.AlbumsCountColumn} = {Artist.AlbumsCountColumn} - 1
    WHERE {Artist.IdColumn} = {Old}.{Album.ArtistIdColumn};
END;

CREATE TRIGGER [{AlbumDeleteTrigger}]
  AFTER DELETE ON [{Album.Table}]
BEGIN
  UPDATE {Artist.Table} 
    SET {Artist.AlbumsCountColumn} = {Artist.AlbumsCountColumn} - 1
    WHERE {Artist.IdColumn} = {Old}.{Album.ArtistIdColumn};
END;";

            ExecuteScalar(cmd);
        }

        private void CreateTrackTable()
        {
            var cmd = $@"
CREATE TABLE [{Track.Table}] (
  [{Track.IdColumn          }]  INTEGER  NOT NULL  UNIQUE,
  [{Track.NameColumn        }]  TEXT     NOT NULL  UNIQUE, 
  [{Track.RatingColumn      }]  INTEGER  NOT NULL  DEFAULT {Track.DefaultRating}, 
  [{Track.TagColumn         }]  TEXT     NOT NULL  DEFAULT {Track.DefaultTag.AsSqlString()},
  [{Track.ArtistIdColumn    }]  INTEGER  NOT NULL  DEFAULT {Artist.DefaultId}, 
  [{Track.AlbumIdColumn     }]  INTEGER  NOT NULL  DEFAULT {Album.DefaultId},
  [{Track.AlbumNumberColumn }]  INTEGER  NOT NULL  DEFAULT {Track.DefaultAlbumNumber},
  [{Track.GenreIdColumn     }]  INTEGER  NOT NULL  DEFAULT {Genre.DefaultId},
  [{Track.DurationColumn    }]  INTEGER  NOT NULL  DEFAULT {Track.DefaultDuration},
  [{Track.UriColumn         }]  VARCHAR  NOT NULL  UNIQUE,

  PRIMARY KEY ([{Track.IdColumn}]),

  FOREIGN KEY ([{Track.ArtistIdColumn}]) 
    REFERENCES [{Artist.Table}]([{Artist.IdColumn}])
    ON DELETE SET DEFAULT,

  FOREIGN KEY ([{Track.AlbumIdColumn}])
    REFERENCES [{Album.Table}]([{Album.IdColumn}])
    ON DELETE SET DEFAULT,

  FOREIGN KEY ([{Track.GenreIdColumn}]) 
    REFERENCES [{Genre.Table}]([{Genre.IdColumn}])
    ON DELETE SET DEFAULT);

CREATE TRIGGER [{TrackInsertTrigger}]
  AFTER INSERT ON [{Track.Table}]
BEGIN
  UPDATE {Artist.Table} 
    SET {Artist.TracksCountColumn} = {Artist.TracksCountColumn} + 1
    WHERE {Artist.IdColumn} = {New}.{Track.IdColumn};
  UPDATE {Album.Table} 
    SET {Album.TracksCountColumn } = {Album.TracksCountColumn } + 1 
    WHERE {Album.IdColumn } = {New}.{Track.AlbumIdColumn};
END;

CREATE TRIGGER [{TrackUpdateTrigger}] 
  AFTER UPDATE ON [{Track.Table}]
  WHEN {New}.{Track.ArtistIdColumn} != {Old}.{Track.ArtistIdColumn} 
  OR {New}.{Track.AlbumNumberColumn} != {Old}.{Track.AlbumIdColumn}
BEGIN
  UPDATE {Artist.Table} 
    SET {Artist.TracksCountColumn} = {Artist.TracksCountColumn} + 1
    WHERE {Artist.IdColumn} = {New}.{Track.ArtistIdColumn};
  UPDATE {Artist.Table}
    SET {Artist.TracksCountColumn} = {Artist.TracksCountColumn} - 1
    WHERE {Artist.IdColumn} = {Old}.{Track.ArtistIdColumn};
  UPDATE {Album.Table}
    SET {Album.TracksCountColumn} = {Album.TracksCountColumn} + 1
    WHERE {Album.IdColumn} = {New}.{Track.AlbumIdColumn};
  UPDATE {Album.Table}
    SET {Album.TracksCountColumn} = {Album.TracksCountColumn} - 1
    WHERE {Album.IdColumn} = {Old}.{Track.AlbumIdColumn};
END;

CREATE TRIGGER [{TrackDeleteTrigger}]
  AFTER DELETE ON [{Track.Table}] 
BEGIN
  UPDATE {Artist.Table} 
    SET {Artist.TracksCountColumn} = {Artist.TracksCountColumn} - 1 
    WHERE {Artist.IdColumn} = {Old}.{Track.ArtistIdColumn};
  UPDATE {Album.Table} 
    SET {Album.TracksCountColumn } = {Album.TracksCountColumn } - 1 
    WHERE {Album.IdColumn } = {Old}.{Track.AlbumIdColumn};
END;";

            ExecuteScalar(cmd);
        }

        private void CreatePlaylistTable()
        {
            var cmd = $@"
CREATE TABLE [{Playlist.Table}]
(
  [{Playlist.IdColumn           }]  INTEGER  NOT NULL  UNIQUE,
  [{Playlist.NameColumn         }]  TEXT     NOT NULL  UNIQUE,
  [{Playlist.RatingColumn       }]  INTEGER  NOT NULL  DEFAULT {Playlist.DefaultRating},
  [{Playlist.TagColumn          }]  TEXT     NOT NULL  DEFAULT {Playlist.DefaultTag.AsSqlString()},
  [{Playlist.TracksCountColumn  }]  INTEGER  NOT NULL  DEFAULT 0,

  PRIMARY KEY ([{Playlist.IdColumn}])
);

CREATE TABLE [{PlaylistMap.Table}]
(
  [{PlaylistMap.IdColumn}] INTEGER NOT NULL UNIQUE,
  [{PlaylistMap.PlaylistIdColumn}] INTEGER NOT NULL,
  [{PlaylistMap.TrackIdColumn}] INTEGER NOT NULL,

  PRIMARY KEY ([{PlaylistMap.IdColumn}]),
  UNIQUE ([{PlaylistMap.PlaylistIdColumn}], [{PlaylistMap.TrackIdColumn}]),
  FOREIGN KEY ([{PlaylistMap.PlaylistIdColumn}])
    REFERENCES [{Playlist.Table}]([{Playlist.IdColumn}])
    ON DELETE CASCADE,
  FOREIGN KEY ([{PlaylistMap.TrackIdColumn}])
    REFERENCES [{Track.Table}]([{Track.IdColumn}])
    ON DELETE CASCADE
);

CREATE TRIGGER [{PlaylistMapInsertTrigger}] AFTER INSERT ON [{PlaylistMap.Table}]
BEGIN
  UPDATE [{Playlist.Table}]
    SET {Playlist.TracksCountColumn} = {Playlist.TracksCountColumn} + 1
    WHERE {Playlist.IdColumn} = {New}.{PlaylistMap.PlaylistIdColumn};
END;

CREATE TRIGGER [{PlaylistMapUpdateTrigger}]
  AFTER UPDATE ON [{PlaylistMap.Table}]
  WHEN {New}.{PlaylistMap.PlaylistIdColumn} != {Old}.{PlaylistMap.PlaylistIdColumn}
BEGIN
  UPDATE [{Playlist.Table}]
    SET {Playlist.TracksCountColumn} = {Playlist.TracksCountColumn} + 1
    WHERE {Playlist.IdColumn} = {New}.{PlaylistMap.PlaylistIdColumn};
  UPDATE [{Playlist.Table}]
    SET {Playlist.TracksCountColumn} = {Playlist.TracksCountColumn} - 1
    WHERE {Playlist.IdColumn} = {Old}.{PlaylistMap.PlaylistIdColumn};
END;

CREATE TRIGGER [{PlaylistMapDeleteTrigger}] AFTER DELETE ON [{PlaylistMap.Table}]
BEGIN
  UPDATE [{Playlist.Table}]
    SET {Playlist.TracksCountColumn} = {Playlist.TracksCountColumn} - 1
    WHERE {Playlist.IdColumn} = {Old}.{PlaylistMap.PlaylistIdColumn};
END;";

            ExecuteScalar(cmd);
        }

        private void CreateDefaultItems()
        {
            var cmd = $@"
INSERT INTO [{Genre.Table}]  
    ({Genre.IdColumn}, {Genre.NameColumn}, {Genre.TagColumn}) 
VALUES 
    ({Genre.DefaultId}, '{Genre.DefaultName}', '');

INSERT INTO [{Artist.Table}] 
    ({Artist.IdColumn}, {Artist.NameColumn}, {Artist.TagColumn})
VALUES 
    ({Artist.DefaultId}, '{Artist.DefaultName}', '');

INSERT INTO [{Album.Table}] 
    ({Album.IdColumn}, {Album.NameColumn}, {Album.ArtistIdColumn}, {Album.TagColumn}) 
VALUES 
    ({Album.DefaultId}, 'Unknown album', {Artist.DefaultId}, '');";

            ExecuteScalar(cmd);
        }
    }
}
