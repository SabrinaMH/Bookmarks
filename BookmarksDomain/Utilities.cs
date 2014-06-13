using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookmarksDomain
{
    internal static class Utilities
    {
        internal static void SetProperty<T>(ref T field, T value, Bookmark bookmark, PropertyChangedEventHandler propertyChanged, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = propertyChanged;
                if (handler != null)
                {
                    handler(bookmark, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
