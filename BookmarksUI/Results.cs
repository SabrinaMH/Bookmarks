using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarksUI
{
    internal class Results
    {
        private static Results instance = null;
        private BookmarksBLL.Search search; 

        private Results()
        {
            search = BookmarksBLL.Search.Instance;
        }

        internal static Results Instance
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

        internal void ListResults(MainForm form, string input)
        {
            form.LboxResults = search.PerformSearch(input);
        }
    }
}
