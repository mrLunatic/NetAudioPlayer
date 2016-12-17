using System.CodeDom;
using Spartan.ServerCore.Components.DAL.RequestParameters;
using Spartan.ServerNet45.Data;

namespace Spartan.ServerNet45.Components.DAL
{
    public partial class SqliteDal
    {
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
);

CREATE INDEX [{Genre.NameIndex}]
  ON [{Genre.Table}]
  ([{Genre.NameColumn}]);";

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

  PRIMARY KEY ([{Artist.IdColumn}]));

CREATE INDEX [{Artist.NameIndex}]
  ON [{Artist.Table}]
  ([{Artist.NameColumn}]);";

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

CREATE INDEX [{Album.NameIndex}]
  ON [{Album.Table}]
  ([{Album.NameColumn}]);

CREATE INDEX [{Album.ArtistIdIndex}]
  ON [{Album.Table}]
  ([{Album.ArtistIdColumn}]);

CREATE INDEX [{Album.GenreIdIndex}]
  ON [{Album.Table}]
  ([{Album.GenreIdColumn}]);

CREATE TRIGGER [{Album.InsertTrigger}]
  AFTER INSERT
  ON [{Album.Table}]
BEGIN
  UPDATE [{Artist.Table}] 
    SET [{Artist.AlbumsCountColumn}] = [{Artist.AlbumsCountColumn}] + 1
    WHERE [{Artist.IdColumn}] = {New}.[{Album.ArtistIdColumn}];
END;

CREATE TRIGGER [{Album.UpdateTrigger}]
  AFTER UPDATE OF [{Album.ArtistIdColumn}]
  ON [{Album.Table}]
BEGIN 
  UPDATE [{Artist.Table}]
    SET [{Artist.AlbumsCountColumn}] = [{Artist.AlbumsCountColumn}] + 1
    WHERE [{Artist.IdColumn}] = {New}.[{Album.ArtistIdColumn}];
  UPDATE [{Artist.Table}]
    SET [{Artist.AlbumsCountColumn}] = [{Artist.AlbumsCountColumn}] - 1
    WHERE [{Artist.IdColumn}] = {Old}.[{Album.ArtistIdColumn}];
END;

CREATE TRIGGER [{Album.DeleteTrigger}]
  AFTER DELETE
  ON [{Album.Table}]
BEGIN
  UPDATE [{Artist.Table}] 
    SET [{Artist.AlbumsCountColumn}] = [{Artist.AlbumsCountColumn}] - 1
    WHERE [{Artist.IdColumn}] = {Old}.[{Album.ArtistIdColumn}];
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

CREATE INDEX [{Track.NameIndex}]
  ON [{Track.Table}]
  ([{Track.NameColumn}]);

CREATE INDEX [{Track.ArtistIdIndex}]
  ON [{Track.Table}]
  ([{Track.ArtistIdColumn}]);

CREATE INDEX [{Track.AlbumIdIndex}]
  ON [{Track.Table}]
  ([{Track.AlbumIdColumn}]);

CREATE INDEX [{Track.GenreIdIndex}]
  ON [{Track.Table}]
  ([{Track.GenreIdColumn}]);

CREATE TRIGGER [{Track.InsertTrigger}]
  AFTER INSERT
  ON [{Track.Table}]
BEGIN
  UPDATE [{Artist.Table}] 
    SET [{Artist.TracksCountColumn}] = [{Artist.TracksCountColumn}] + 1
    WHERE [{Artist.IdColumn}] = {New}.[{Track.IdColumn}];
  UPDATE [{Album.Table}] 
    SET [{Album.TracksCountColumn}] = [{Album.TracksCountColumn}] + 1 
    WHERE [{Album.IdColumn}] = {New}.[{Track.AlbumIdColumn}];
END;

CREATE TRIGGER [{Track.UpdateArtistIdTrigger}] 
  AFTER UPDATE OF [{Track.ArtistIdColumn}]
  ON [{Track.Table}]
BEGIN
  UPDATE [{Artist.Table}] 
    SET [{Artist.TracksCountColumn}] = [{Artist.TracksCountColumn}] + 1
    WHERE [{Artist.IdColumn}] = {New}.[{Track.ArtistIdColumn}];
  UPDATE [{Artist.Table}]
    SET [{Artist.TracksCountColumn}] = [{Artist.TracksCountColumn}] - 1
    WHERE [{Artist.IdColumn}] = {Old}.[{Track.ArtistIdColumn}];
END;

CREATE TRIGGER [{Track.UpdateAlbumIdTrigger}] 
  AFTER UPDATE OF [{Track.AlbumIdColumn}]
  ON [{Track.Table}]
BEGIN
  UPDATE [{Album.Table}]
    SET [{Album.TracksCountColumn}] = [{Album.TracksCountColumn}] + 1
    WHERE [{Album.IdColumn}] = {New}.[{Track.AlbumIdColumn}];
  UPDATE [{Album.Table}]
    SET [{Album.TracksCountColumn}] = [{Album.TracksCountColumn}] - 1
    WHERE [{Album.IdColumn}] = {Old}.[{Track.AlbumIdColumn}];
END;

CREATE TRIGGER [{Track.DeleteTrigger}]
  AFTER DELETE
  ON [{Track.Table}] 
BEGIN
  UPDATE [{Artist.Table}] 
    SET [{Artist.TracksCountColumn}] = [{Artist.TracksCountColumn}] - 1 
    WHERE [{Artist.IdColumn}] = {Old}.[{Track.ArtistIdColumn}];
  UPDATE [{Album.Table}] 
    SET [{Album.TracksCountColumn}] = [{Album.TracksCountColumn}] - 1 
    WHERE [{Album.IdColumn}] = {Old}.[{Track.AlbumIdColumn}];
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

CREATE INDEX [{Playlist.NameIndex}]
  ON [{Playlist.Table}]
  ([{Playlist.NameColumn}]);

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

CREATE INDEX [{PlaylistMap.PlaylistIdIndex}]
  ON [{PlaylistMap.Table}]
  ([{PlaylistMap.PlaylistIdColumn}]);

CREATE TRIGGER [{PlaylistMap.InsertTrigger}]
  AFTER INSERT
  ON [{PlaylistMap.Table}]
BEGIN
  UPDATE [{Playlist.Table}]
    SET {Playlist.TracksCountColumn} = {Playlist.TracksCountColumn} + 1
    WHERE {Playlist.IdColumn} = {New}.{PlaylistMap.PlaylistIdColumn};
END;

CREATE TRIGGER [{PlaylistMap.UpdatePlaylistIdTrigger}]
  AFTER UPDATE OF [{PlaylistMap.PlaylistIdColumn}]
  ON [{PlaylistMap.Table}]
BEGIN
  UPDATE [{Playlist.Table}]
    SET [{Playlist.TracksCountColumn}] = [{Playlist.TracksCountColumn}] + 1
    WHERE [{Playlist.IdColumn}] = {New}.[{PlaylistMap.PlaylistIdColumn}];
  UPDATE [{Playlist.Table}]
    SET [{Playlist.TracksCountColumn}] = [{Playlist.TracksCountColumn}] - 1
    WHERE [{Playlist.IdColumn}] = {Old}.[{PlaylistMap.PlaylistIdColumn}];
END;

CREATE TRIGGER [{PlaylistMap.DeleteTrigger}]
  AFTER DELETE
  ON [{PlaylistMap.Table}]
BEGIN
  UPDATE [{Playlist.Table}]
    SET [{Playlist.TracksCountColumn}] = [{Playlist.TracksCountColumn}] - 1
    WHERE [{Playlist.IdColumn}] = {Old}.[{PlaylistMap.PlaylistIdColumn}];
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
