using System.Net;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using gisAPI.Interfaces;
using gisAPI.Persistence;
using gisAPI.Persistence.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace gisAPI.Auth
{
    public class RegisterUser
    {
        public class Command : IRequest<User>
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Nama { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        { 
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty().Matches(@"\A\S+\z"); ;
                RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;
            private readonly IPasswordHasher _hasher;

            public Handler(IDbContext context, IMapper mapper, IPasswordHasher hasher)
            {
                _context = context;
                _mapper = mapper;
                _hasher = hasher;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.User.FindAsync(x => x.Username == request.Username);

                if (user != null)
                    throw new ApiException("Username sudah terdaftar", (int)HttpStatusCode.BadRequest);

                var model = _mapper.Map<User>(request);

                byte[] hash;
                byte[] salt;

                _hasher.Generate(request.Password, out hash, out salt);

                model.PwdHash = hash;
                model.PwdSalt = salt;
                model.DateCreate = DateTime.Now;
                model.Role = "Admin";

                Console.WriteLine(JsonConvert.SerializeObject(model).ToString());

                if (!await _context.User.InsertAsync(model))
                    throw new ApiException("Registrasi gagal", (int)HttpStatusCode.BadRequest);

                return model;
            }
        }
    }
}