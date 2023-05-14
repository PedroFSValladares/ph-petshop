using Microsoft.Data.SqlClient;
using PHPetshop.Services.Mail;
using PHPetshop.Services.Persistence;
using PHPetshop.Services.Persistence.Repositories;

namespace PHPetshop.Services.Extensions
{
    public static class ExtensionMethods
    {
        public static void RegisterSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(options => new SqlConnection(connectionString))
                .AddScoped(options => new DbService(options.GetRequiredService<SqlConnection>(), options.GetRequiredService<UsuarioRepository>()))
                .AddScoped(options => new UsuarioRepository(options.GetRequiredService<SqlConnection>()));
        }

        public static MailClientConfiguration GetMailConfiguration(this IConfiguration configuration, string name) {
            var section = configuration.GetSection(name);
            return new MailClientConfiguration {
                Login = section["Login"],
                Password = section["Password"],
                Port = int.Parse(section["Port"]),
                Server = section["Server"]
            };
        }
    }
}
