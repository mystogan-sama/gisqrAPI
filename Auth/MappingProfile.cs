using gisAPI.Persistence.Repositories;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace gisAPI.Auth
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUser.Command, User>();
            CreateMap<User, LoginDto>()
              .ForMember(d => d.accessToken, opt => opt.Ignore());
        }
    }



    public class LoginDto
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? role { get; set; }
        public string? accessToken { get; set; }
        public string? avatar { get; set; }
        public string? email { get; set; }
    }

    public class QrUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
    }

    public class QrUserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Role { get; set; }
    }

    public class QrUserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class QrUserEditDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Role { get; set; }
    }

    public class QrUserChangePassDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}