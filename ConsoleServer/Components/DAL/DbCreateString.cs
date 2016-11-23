using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetAudioPlayer.Core.Data;

namespace NetAudioPlayer.ConsoleServer.Components.DAL
{
    public partial class SqliteDal
    {

        static readonly string DbScript = $@"
CREATE TABLE [{GenreTable}]
(
  [{Genre.IdField}]             INTEGER         NOT NULL UNIQUE,
  [{Genre.NameField}]           VARCHAR(64)     NOT NULL UNIQUE,
  [{Genre.RatingField}]         INTEGER         NOT NULL DEFAULT 0,
  [{Genre.TagField}]            VARCHAR(64),

  PRIMARY KEY ([{Genre.IdField}])                       C:\Users\Дмитрий\Source\Repos\NetAudioPlayer\ConsoleServer\Components\DAL\DbCreateString.cs
);

CREATE TABLE [{ArtistTable}] 
(
  [{Artist.IdField}]            INTEGER         NOT NULL UNIQUE,
  [{Artist.NameField}]          VARCHAR(64)     NOT NULL UNIQUE,
  [{Artist.AlbumsCountField}]   INTEGER         NOT NULL DEFAULT 0,
  [{Artist.TracksCountField}]   INTEGER         NOT NULL DEFAULT 0,
  [{Artist.RatingField}]        INTEGER         NOT NULL DEFAULT 0,
  [{Artist.TagField}]           VARCHAR(64),

  PRIMARY KEY  ([{Artist.IdField}])
);

CREATE TABLE [{AlbumTable}]
(
  [{Album.IdField}]             INTEGER         NOT NULL UNIQUE,
  [{Album.NameField}]           VARCHAR(64)     NOT NULL UNIQUE,
  [{Album.ArtistIdField}]       INTEGER         NOT NULL DEFAULT 0,
  [{Album.YearField}]           INTEGER         NOT NULL DEFAULT 1900,
  [{Album.TracksCountField}]    INTEGER         NOT NULL DEFAULT 0, 
  [{Album.RatingField}]         INTEGER         NOT NULL DEFAULT 0, 
  [{Album.TagField}]            VARCHAR(64),

  PRIMARY KEY ([{Album.IdField}]),

  FOREIGN KEY ([{Album.ArtistIdField}])
    REFERENCES [{ArtistTable}]([{Artist.IdField}]) ON DELETE SET DEFAULT
);

CREATE TRIGGER {AlbumTable}_insert_trigger AFTER INSERT ON [{AlbumTable}]
  BEGIN
  UPDATE {ArtistTable} 
    SET {Artist.AlbumsCountField} = {Artist.AlbumsCountField} + 1
    WHERE {Artist.IdField} = NEW.{Album.ArtistIdField};
  END;

CREATE TRIGGER {AlbumTable}_delete_trigger AFTER DELETE ON [{AlbumTable}]
  BEGIN
    UPDATE {ArtistTable} 
    SET {Artist.AlbumsCountField} = {Artist.AlbumsCountField} - 1
    WHERE {Artist.IdField} = OLD.{Album.ArtistIdField};
  END;

CREATE TABLE [{TrackTable}]
(
  [{Track.IdField}]             INTEGER         NOT NULL UNIQUE,
  [{Track.NameField}]           VARCHAR(64)     NOT NULL UNIQUE, 
  [{Track.ArtistIdField}]       INTEGER         NOT NULL DEFAULT 0, 
  [{Track.AlbumIdField}]        INTEGER         NOT NULL DEFAULT 0,
  [{Track.GenreIdField}]        INTEGER         NOT NULL DEFAULT 0,
  [{Track.DurationField}]       INTEGER         NOT NULL DEFAULT 0,
  [{Track.UriField}]            VARCHAR         NOT NULL UNIQUE,
  [{Track.RatingField}]         INTEGER         NOT NULL DEFAULT 0, 
  [{Track.TagField}]            VARCHAR(64),

  PRIMARY KEY ([{Track.IdField}]),

  FOREIGN KEY ([{Track.ArtistIdField}]) 
    REFERENCES [{ArtistTable}]([{Artist.IdField}]) ON DELETE SET DEFAULT,

  FOREIGN KEY ([{Track.AlbumIdField}])
    REFERENCES [{AlbumTable}]([{Album.IdField}]) ON DELETE SET DEFAULT,

  FOREIGN KEY ([{Track.GenreIdField}]) 
    REFERENCES [{GenreTable}]([{Genre.IdField}]) ON DELETE SET DEFAULT
);


CREATE TRIGGER [{TrackTable}_insert_trigger] AFTER INSERT ON [{TrackTable}]
  BEGIN
  UPDATE {ArtistTable} 
    SET {Artist.TracksCountField} = {Artist.TracksCountField} + 1
    WHERE {Artist.IdField} = NEW.{Track.IdField};
  UPDATE {AlbumTable } 
    SET {Album.TracksCountField } = {Album.TracksCountField } + 1 
    WHERE {Album.IdField } = NEW.{Track.AlbumIdField};
  END;

CREATE TRIGGER [{TrackTable}_delete_trigger] AFTER DELETE ON [{TrackTable}] 
  BEGIN
  UPDATE {ArtistTable} 
    SET {Artist.TracksCountField} = {Artist.TracksCountField} - 1 
    WHERE {Artist.IdField} = OLD.{Track.ArtistIdField};
  UPDATE {AlbumTable } 
    SET {Album.TracksCountField } = {Album.TracksCountField } - 1 
    WHERE {Album.IdField } = OLD.{Track.AlbumIdField};
  END;

CREATE VIEW [{TrackTable}_v] AS
  SELECT [{TrackTable}].*, 
         [{ArtistTable}].[{Artist.NameField}] as {Track.ArtistNameField}, 
         [{AlbumTable}].[{Album.NameField}] as {Track.ArtistNameField}, 
         [{GenreTable}].[{Genre.NameField}] as {Track.GenreNameField}
  FROM [{TrackTable}], [{ArtistTable}], [{AlbumTable}], [{GenreTable}]
  WHERE 
    [{TrackTable}].[{Track.ArtistIdField}] = [{ArtistTable}].[{Artist.IdField}] AND 
    [{TrackTable}].[{Track.AlbumIdField}] = [{AlbumTable}].[{Album.IdField}] AND 
    [{TrackTable}].[{Track.GenreIdField}] = [{GenreTable}].[{Genre.IdField}];

INSERT INTO [{GenreTable}]  ({Genre.IdField}, {Genre.NameField}, {Genre.TagField}) 
    VALUES ({Genre.DefaultId}, 'Unknown genre', 'default');

INSERT INTO [{ArtistTable}] ({Artist.IdField}, {Artist.NameField}, {Artist.TagField})
    VALUES ({Artist.DefaultId}, 'Unknown artist', 'default');

INSERT INTO [{AlbumTable}] ({Album.IdField}, {Album.NameField}, {Album.ArtistIdField}, {Album.TagField}) 
    VALUES ({Album.DefaultId}, 'Unknown album', 0, 'default');";


    }
}
