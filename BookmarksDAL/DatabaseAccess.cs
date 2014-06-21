using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate;
using BookmarksDomain;
using NHibernate.Transform;

namespace BookmarksDAL
{
    public class DatabaseAccess
    {
        private PersistenceManager persistenceManager;
        private ISession session;

        public DatabaseAccess()
        {
            persistenceManager = new PersistenceManager();
            this.session = persistenceManager.session;
        }

        // Implement using database object such that it doesn't know about the bookmarks class nor about how how the UI layer represents the bookmarks. Thus just return the bookmarks themselves.
        public IList<Bookmark> GetDataForResults(string input)
        {
            string query = @"
                select moz_bookmarks.title, moz_bookmarks.id, moz_places.url, moz_items_annos.content
                from moz_places 
                inner join moz_bookmarks on moz_places.id = moz_bookmarks.fk
                left outer join moz_items_annos on moz_bookmarks.id = moz_items_annos.item_id 
                where moz_places.url like 'http%' and moz_bookmarks.title like '%" + input + "%'";

            IQuery sqlQuery = session.CreateSQLQuery(query).SetResultTransformer(Transformers.AliasToBean(typeof(Bookmark)));
            return sqlQuery.List<Bookmark>();
        }

        public void Close()
        {
            persistenceManager.Close();
        }
    }
}
