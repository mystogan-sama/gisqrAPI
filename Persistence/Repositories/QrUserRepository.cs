using System.Data;
using gisAPI.Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace gisAPI.Persistence.Repositories
{
    public class QrUserRepository : DapperRepository<WebUser>
    {
        public QrUserRepository(IDbConnection connection) : base(connection) { }
    public QrUserRepository(IDbConnection connection, ISqlGenerator<WebUser> sqlGenerator) : base(connection, sqlGenerator) { }
    }
}