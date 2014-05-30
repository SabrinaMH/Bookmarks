using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookmarksDAL;

namespace BookmarksBLL
{
    public class Results
    {
        private static Results instance = null;
        private DatabaseConnection databaseConnection;
        private Dictionary<string, Bookmark> results;

        private Results()
        {
            databaseConnection = DatabaseConnection.Instance;
        }

        public static Results Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Results();
                }

                return instance;
            }
        }

        public List<string> PerformSearch(string partOfTitle)
        {
            results = databaseConnection.GetBookmarks(partOfTitle);
            return results.Select(b => b.Key).ToList();
        }

        public string[] GetData(string title)
        {
            results[title].
            results[]
        }
    }
}
