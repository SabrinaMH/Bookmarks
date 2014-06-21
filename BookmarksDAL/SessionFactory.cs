using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using System.ServiceModel.Channels;
using System.Configuration;

namespace NHibernateExperiment
{
    public sealed class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();

        public static NHibernate.ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            Configuration configuration = new Configuration();
                            configuration.AddAssembly(Assembly.GetCallingAssembly());
                            iSessionFactory = configuration.BuildSessionFactory();
                        }
                    }
                }

                return iSessionFactory.OpenSession();
            }
        }
    }
}