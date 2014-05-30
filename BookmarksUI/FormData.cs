using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarksUI
{
    internal class FormData
    {
        private static FormData instance = null;
        private BookmarksBLL.Results results; 

        private FormData()
        {
            results = BookmarksBLL.Results.Instance;
        }

        internal static FormData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FormData();
                }

                return instance;
            }
        }

        internal void ListResults(MainForm form, string partOfTitle)
        {
            form.LboxResults = results.PerformSearch(partOfTitle);
        }

        internal void ListData(MainForm form, string title)
        {
            string[] data = results.GetData(title);
            form.TxtUrl = data[0];
            form.TxtComment = data[1];
        }
    }
}
