CREATE TABLE [genre] (
  [id]       INTEGER         NOT NULL UNIQUE, 
  [name]    VARCHAR(64) NOT NULL UNIQUE, 
  [rating]   INTEGER         NOT NULL DEFAULT 0, 
  [tag]     VARCHAR(64), 
  PRIMARY KEY ([id])
);

CREATE TABLE [artist] (
  [id]                 INTEGER         NOT NULL UNIQUE, 
  [name]              VARCHAR(64) NOT NULL UNIQUE, 
  [albumsCount]        INTEGER         NOT NULL DEFAULT 0, 
  [trackCount]         INTEGER         NOT NULL DEFAULT 0, 
  [rating]             INTEGER         NOT NULL DEFAULT 0, 
  [tag]               VARCHAR(64),  
  PRIMARY KEY  ([id])
);

CREATE TABLE [album] (
  [id]                INTEGER          NOT NULL UNIQUE, 
  [name]             VARCHAR(64)  NOT NULL UNIQUE, 
  [artistId]          INTEGER          NOT NULL DEFAULT 0, 
  [year]              INTEGER          NOT NULL DEFAULT 1900, 
  [trackCount]        INTEGER          NOT NULL DEFAULT 0, 
  [rating]            INTEGER          NOT NULL DEFAULT 0, 
  [tag]              VARCHAR(64), 
  PRIMARY KEY ([id]),
  FOREIGN KEY ([artistId]) REFERENCES [artist]([id]) 
             ON DELETE SET DEFAULT
);

CREATE TABLE [track] (
  [id]                INTEGER          NOT NULL UNIQUE, 
  [name]             VARCHAR(64)  NOT NULL UNIQUE, 
  [artistId]          INTEGER          NOT NULL DEFAULT 0, 
  [albumId]           INTEGER          NOT NULL DEFAULT 0,
  [genreId]           INTEGER          NOT NULL DEFAULT 0,
  [duration]          INTEGER          NOT NULL DEFAULT 0,
  [uri]              VARCHAR      NOT NULL UNIQUE,
  [rating]            INTEGER          NOT NULL DEFAULT 0, 
  [tag]              VARCHAR(64), 
  PRIMARY KEY ([id]),
  FOREIGN KEY ([artistId]) REFERENCES [artist]([id]) 
             ON DELETE SET DEFAULT,
  FOREIGN KEY ([albumId]) REFERENCES [album]([id]) 
             ON DELETE SET DEFAULT
  FOREIGN KEY ([genreId]) REFERENCES [genre]([id]) 
             ON DELETE SET DEFAULT
);