using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Doing.Common.Auth
{
    public static class Extensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services,
                IConfiguration configuration)
        {
            var options = new JwtOptions();

            var section = configuration.GetSection("jwt");

            section.Bind(options);

            services.Configure<JwtOptions>(section);

            services.AddSingleton<IJwtHandler, JwtHandler>();

            services.AddAuthentication()
                    .AddJwtBearer(c => {

                        c.RequireHttpsMetadata = false;
                        c.SaveToken = true;
                        c.TokenValidationParameters = new TokenValidationParameters
                        {
                           
                            ValidateAudience = false,
                            ValidIssuer = options.IssuerOfToken,
                            IssuerSigningKey = 
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
          
                        };
                    });

            return services;
        }
    }
}