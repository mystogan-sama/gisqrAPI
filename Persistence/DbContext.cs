using System.Data;
using System.Data.SqlClient;
using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using gisAPI.Persistence.Repositories;


namespace gisAPI.Persistence
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Connection { get; }
        QrUserRepository WebUser { get; }
        UserRepository User { get; }


    }
    public class DbContext : IDbContext
    {
        private bool _disposed;

        public DbContext(string connectionString)
        {
            MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }
        public IDbConnection Connection { get; }
        public QrUserRepository WebUser => new QrUserRepository(Connection);
        public UserRepository User => new UserRepository(Connection);



        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Connection?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DbContext()
        {
            Dispose(false);
        }
    }
}