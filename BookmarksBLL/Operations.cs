using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookmarksDAL;
using BookmarksDomain;

namespace BookmarksBLL
{
    public class Operations
    {
        private static Operations instance = null;
        //private DatabaseConnection databaseConnection;
        private DatabaseAccess databaseAccess;
        private IBookmarksView view;
        internal List<Bookmark> Results { get; private set; }

        private Operations()
        {
            //databaseConnection = DatabaseConnection.Instance;
            databaseAccess = new DatabaseAccess();
        }

        public static Operations Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Operations();
                }

                return instance;
            }
        }

        public IBookmarksView View
        { 
            get 
            { 
                return view; 
            } 
            set
            { 
                view = value;
                view.Click += delegate { PerformSearch(view.PartOfTitle); };
            }
        }

        // old version returned List<Bookmark>
        public IList<Bookmark> PerformSearch(string partOfTitle) // todo: should have own class together with CreateBookmarks
        {
            return databaseAccess.GetDataForResults(partOfTitle);
            
            //List<string> dataForResults = databaseConnection.GetDataForResults(partOfTitle);
            //Results = CreateBookmarks(dataForResults);
            //return Results;
        }

        /*
        private List<Bookmark> CreateBookmarks(List<string> data)
        {
            var bookmarks = new List<Bookmark>();

            for (int i = 0; i < data.Count(); i += 4)
            {
                bookmarks.Add(new Bookmark(data[i], data[i + 1], data[i + 2], data[i + 3]));
            }

            return bookmarks;
        }
        */

        public string[] GetDataForEntry(Bookmark bookmark)
        {
            return new string[] { bookmark.Url, bookmark.Content };
        }

        public void Delete(Bookmark bookmark)
        {
            databaseAccess.Delete(bookmark);
        }

        /*
        public int Delete(int rowNo)
        {
            int rowsAffected = databaseConnection.Delete(Convert.ToInt32(Results[rowNo].Id));
            if (rowsAffected > 0)
            {
                Results.RemoveAt(rowNo);
            }
            return rowsAffected;
        }
         * */

        public void Add(string title, string url, string comment)
        {
            //databaseAccess.Add(new Bookmark(title, url, comment));
        }
    }
}
