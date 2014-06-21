using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Transform;
using BookmarksDomain;

[assembly: log4net.Config.XmlConfigurator(Watch=true)]
namespace BookmarksDAL
{
    public class PersistenceManager : IDisposable
    {
        private static string filenameCfgXml = "bookmark.cfg.xml";
        private static string pathToCfgXml = System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\BookmarksDAL\";
        private ISessionFactory sessionFactory;
        internal ISession session { get; private set; }

        public PersistenceManager()
        {
            ConfigureLog4Net();
            ConfigureNHibernate();
        }

        /*
        /// <summary>
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<Bookmark> RetrieveMatches(string input)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                string sql = @"
                    select moz_bookmarks.title as ""Title"", moz_bookmarks.id as ""Id"", moz_places.url as ""Url"", moz_items_annos.content as ""Comment""
                    from moz_places 
                    inner join moz_bookmarks on moz_places.id = moz_bookmarks.fk
                    left outer join moz_items_annos on moz_bookmarks.id = moz_items_annos.item_id 
                    where moz_places.url like 'http%' and moz_bookmarks.title like '%" + input + "%'";

                IQuery sqlQuery = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean(typeof(Bookmark)));
                return sqlQuery.List<Bookmark>();
            }
        }
         * */

        public void Dispose()
        {
            sessionFactory.Dispose();
        }

        // Todo: Examine why we both need Dispose and Close.
        /// <summary>
        /// Close this Persistence Manager and release all resources (connection pools etc). It is the responsibility of the application to ensure that there are no open Sessions before calling Close().
        /// </summary>
        public void Close()
        {
            sessionFactory.Close();
        }

        private void ConfigureLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private void ConfigureNHibernate()
        {
            // Initialize
            Configuration cfg = new Configuration();
            cfg.Configure(pathToCfgXml + filenameCfgXml);

            // Add class mappings to configuration object
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            // Create session factory from configuration object
            sessionFactory = cfg.BuildSessionFactory();
            session = sessionFactory.OpenSession();
        }
    }
}
