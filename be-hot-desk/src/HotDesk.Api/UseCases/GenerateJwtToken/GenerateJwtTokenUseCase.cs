using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Common.Options;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HotDesk.Api.UseCases.GenerateJwtToken
{
    public static class GenerateJwtTokenUseCase
    {
        public record Command(int UserId) : IRequest<string>;
        internal class Handler : IRequestHandler<Command, string>
        {
            private readonly IOptions<JwtSettings> jwtOptions;
            private readonly IRepository repository;
            private readonly TimeSpan tokenLifetime = TimeSpan.FromHours(2);

            public Handler(IOptions<JwtSettings> jwtOptions, IRepository repository)
            {
                this.jwtOptions = jwtOptions;
                this.repository = repository;
            }
            public Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtOptions.Value.Key!);
                var matchingEmployee = repository.Employees
                    .Include(x => x.Roles)
                    .FirstOrDefault(x => x.Id == request.UserId) ?? throw new ArgumentNullException($"Did not find user id: {request.UserId}");

                var roleNames = matchingEmployee.Roles!.Select(x => x.Name).ToList();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                    new Claim("roles", string.Join(";", roleNames))
                };


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(tokenLifetime),
                    Issuer = jwtOptions.Value.Issuer,
                    Audience = jwtOptions.Value.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var jwt = tokenHandler.WriteToken(token);
                return Task.FromResult(jwt);
            }
        }
    }
}
