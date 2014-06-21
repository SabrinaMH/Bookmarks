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

        //private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\Profiles\cjm5z3n9.default\places.sqlite");
        //private SQLiteConnectionStringBuilder builder;

        public DatabaseAccess()
        {
            persistenceManager = new PersistenceManager();
            this.session = persistenceManager.session;
        }

        /*
        private DatabaseConnection()
        {
            builder = new SQLiteConnectionStringBuilder();
            builder.FailIfMissing = true;
            builder.DataSource = dbPath;
        }
         * */
       

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
        
    /*
    public List<string> GetDataForResults(string input)
    {
        List<string> results = new List<string>();

        using (SQLiteConnection conn = new SQLiteConnection(builder.ConnectionString))
        {
            conn.Open();

            string query = @"
                select moz_bookmarks.title, moz_bookmarks.id, moz_places.url, moz_items_annos.content
                from moz_places 
                inner join moz_bookmarks on moz_places.id = moz_bookmarks.fk
                left outer join moz_items_annos on moz_bookmarks.id = moz_items_annos.item_id 
                where moz_places.url like 'http%' and moz_bookmarks.title like '%" + input + "%'";

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var title = reader.GetString(0);
                        var id = reader.GetInt32(1);
                        var url = reader.GetString(2);
                        var comment = (reader.GetValue(3) != DBNull.Value) ? reader.GetString(3) : "";
                        results.Add(title);
                        results.Add(id.ToString());
                        results.Add(url);
                        results.Add(comment);
                    }
                }
            }
            conn.Close();
        }

        return results;
    }
     * */

        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void Delete(Bookmark bookmark)
        {
            using (session.BeginTransaction())
            {
                session.Delete(bookmark);
                session.Transaction.Commit();
            }
        }

        public void Save(Bookmark bookmark)
        {
            using (session.BeginTransaction())
            {
                session.Save(bookmark);
            }
        }
    }
}
