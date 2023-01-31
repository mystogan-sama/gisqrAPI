using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using gisAPI.Interfaces;
using gisAPI.Persistence;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;

namespace gisAPI.Auth
{
    public class LoginUser
    {
        public class Command : IRequest<LoginDto>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, LoginDto>
        {
            private readonly IDbContext _context;
            private readonly IJwtGenerator _jwt;
            private readonly IPasswordHasher _hasher;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _accessor;

            public Handler(IDbContext context, IJwtGenerator jwt, IPasswordHasher hasher, IMapper mapper, IUserAccessor accessor)
            {
                _context = context;
                _jwt = jwt;
                _hasher = hasher;
                _mapper = mapper;
                _accessor = accessor;
            }

            public async Task<LoginDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.User.FindAsync(x => x.Username == request.Username);

                if (user == null || !_hasher.Verify(request.Password, user.PwdHash, user.PwdSalt))
                    throw new ApiException("Login gagal. Harap cek username dan password anda.", (int)HttpStatusCode.Unauthorized);
                var result = _mapper.Map<LoginDto>(user);

                result.Id = user.Id;
                result.Username = user.Username.Trim();
                result.FullName = user.Nama.Trim();
                result.role = user.Role.Trim();
                result.avatar = "/img/13-small.d796bffd.png";
                result.accessToken = _jwt.CreateToken(user);
                result.email = user.Username.Trim() + "@insaba.co.id";


                // var hash = new HMACSHA256(Encoding.UTF8.GetBytes("123456binacash123456binacash123456"));
                // var computed = hash.ComputeHash(Encoding.UTF8.GetBytes(token));
                // result.Token = BitConverter.ToString(computed).Replace("-", string.Empty).ToLower();

                user.LastToken = result.accessToken;

                if (!await _context.User.UpdateAsync(user))
                    throw new ApiException("Update token gagal.", (int)HttpStatusCode.Unauthorized);

                return result;
            }

        }
    }
}
