using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace gisAPI.Domain
{
    [Table("WEB_USER")]
    public class WebUser
    {
        [Key, Identity]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Role { get; set; }   
    }
}