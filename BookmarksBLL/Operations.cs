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
        private DatabaseAccess databaseAccess;
        private IBookmarksView view;
        internal IList<Bookmark> Results { get; private set; }

        private Operations()
        {
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

        public IList<Bookmark> PerformSearch(string partOfTitle)
        {
            Results = databaseAccess.GetDataForResults(partOfTitle);
            return Results;
        }

        public void Close()
        {
            databaseAccess.Close();
        }

        public string[] GetDataForEntry(Bookmark bookmark)
        {
            return new string[] { bookmark.Url, bookmark.Content };
        }
    }
}
