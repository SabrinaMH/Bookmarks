using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookmarksDomain
{
    public class Bookmark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string title, url, content;
        private Int64 id;

        public Bookmark() { } // Empty constructor needed for nHibernate

        public Bookmark(string title, string url, string comment)
        {
            this.Title = title;
            this.Url = url;
            this.Content = comment;
        }

        public virtual Int64 Id
        {
            get { return this.id; }
            private set { Utilities.SetProperty(ref id, value, this, PropertyChanged); }
        }

        public virtual string Title
        {
            get { return this.title; }
            private set { Utilities.SetProperty(ref title, value, this, PropertyChanged); }
        }

        public virtual string Url
        {
            get { return this.url; }
            private set { Utilities.SetProperty(ref url, value, this, PropertyChanged); }
        }

        public virtual string Content
        {
            get { return this.content; }
            private set { Utilities.SetProperty(ref content, value, this, PropertyChanged); }
        }
         
        public override string ToString()
        {
            return Title;
        }
    }
}
