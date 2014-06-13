using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarksDomain
{
    public interface IBookmarksView
    {
        event EventHandler Click;
        string PartOfTitle { get; }
        BindingList<Bookmark> Results { set; }
    }
}
