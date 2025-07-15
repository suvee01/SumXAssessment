using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SumXAssessment.API.Services
{
    public static class AuthenticationService
    {
        public static void AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKey = configuration.GetValue<string>("Jwt:Key");
            var key = Encoding.ASCII.GetBytes(jwtKey);

            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                });
        }

    }
}
