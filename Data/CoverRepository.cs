using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Data
{
    public class CoverRepository
    {
        static ConnectionPool connectionPool = new ConnectionPool();

        public static List<Cover> All()
        {
            List<Cover> covers = new List<Cover>();
            SQLiteCommand command = new SQLiteCommand("select * from covertype", connectionPool.DbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                covers.Add(new Cover()
                {
                    Id = reader.GetInt32(0),
                    Elevation = reader.GetInt32(1),
                    Aspect = reader.GetInt32(2),
                    Slope = reader.GetInt32(3),
                    HorizontalDistanceToHydrology = reader.GetInt32(4),
                    VerticalDistanceToHydrology = reader.GetInt32(5),
                    HorizontalDistanceToRoadways =  reader.GetInt32(6),
                    Hillshade9Am = reader.GetInt32(7),
                    HillshadeNoon = reader.GetInt32(8),
                    Hillshade3Pm = reader.GetInt32(9),
                    HorizontalDistanceToFirePoints = reader.GetInt32(10),
                    CoverType = reader.GetInt32(11),
                });
            }

            return covers.OrderBy(c => c.Id).ToList();
        }
    }
}