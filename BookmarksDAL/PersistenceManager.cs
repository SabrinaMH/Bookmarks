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

        public void Dispose()
        {
            sessionFactory.Dispose();
        }

        /// <summary>
        /// Close this Persistence Manager and release all resources (connection pools etc). It is the responsibility of the application to ensure that there are no open Sessions before calling Close().
        /// </summary>
        public void Close()
        {
            sessionFactory.Close();
            Dispose();
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
