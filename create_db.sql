-- SQLite
DROP TABLE IF EXISTS covertype;

CREATE TABLE covertype(
   id INTEGER PRIMARY KEY AUTOINCREMENT,
   elevation INTEGER,
   aspect INTEGER,
   slope INTEGER,
   horizontal_distance_to_hydrology INTEGER,
   vertical_distance_to_hydrology INTEGER,
   horizontal_distance_to_roadways INTEGER,
   hillshade_9am INTEGER,
   hillshade_noon INTEGER,
   hillshade_3pm INTEGER,
   horizontal_distance_to_fire_points INTEGER,
   cover_type INTEGER);

.mode csv covertype
.import covtype_parsed.csv covertype

.schema covertype      
SELECT * FROM covertype;
