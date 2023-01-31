using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace gisAPI.Persistence.Repositories
{
    [Table("WEB_USER")]
    public class User
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Username { get; set; }
        public byte[] PwdHash { get; set; }
        public byte[] PwdSalt { get; set; }
        public string Nama { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? LastToken { get; set; }
        public string? Role { get; set; }

    }

    public class UserRepository : DapperRepository<User>
    {
        public UserRepository(IDbConnection connection) : base(connection) { }
        public UserRepository(IDbConnection connection, ISqlGenerator<User> sqlGenerator) : base(connection, sqlGenerator) { }

    }
}