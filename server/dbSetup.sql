CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE albums(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title TINYTEXT NOT NULL,
  description TEXT,
  cover_img TEXT NOT NULL,
  archived BOOLEAN NOT NULL DEFAULT false,
  category ENUM('aesthetics', 'games', 'animals', 'food', 'vibes', 'misc'),
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

CREATE TABLE pictures(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  img_url TEXT NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  album_id INT NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE
);


CREATE TABLE watchers(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  account_id VARCHAR(255) NOT NULL,
  album_id INT NOT NULL,
  FOREIGN KEY (account_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE,
  -- NOTE you can only watch an album once
  UNIQUE(account_id, album_id)
);

DROP TABLE albums;

SELECT * FROM accounts;

SELECT * FROM albums WHERE archived = true;

SELECT
albums.*,
accounts.*
FROM albums
INNER JOIN accounts ON accounts.id = albums.creator_id
WHERE albums.id = 1;


SELECT * FROM pictures;

SELECT * FROM pictures WHERE id = 3 LIMIT 1;

SELECT * FROM albums;

INSERT INTO watchers(album_id, account_id) VALUES (31, '67ffe04bbdbf63a799434840');

SELECT
watchers.*,
accounts.*
FROM watchers
INNER JOIN accounts ON accounts.id = watchers.account_id
WHERE watchers.album_id = 31;