using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookmarksDAL;

namespace BookmarksBLL
{
    public class Search
    {
        private static Search instance = null;
        private DatabaseConnection databaseConnection;

        private Search()
        {
            databaseConnection = DatabaseConnection.Instance;
        }

        public static Search Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Search();
                }

                return instance;
            }
        }

        public List<string> PerformSearch(string input)
        {
            var results = databaseConnection.GetAllBookmarks(input);
            return results.Select(b => b.Title).ToList();
        }
    }
}
