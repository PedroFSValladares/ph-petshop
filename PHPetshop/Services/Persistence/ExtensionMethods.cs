namespace PHPetshop.Services.Persistence {
    public static class ExtensionMethods {
        public static void RegisterSqlServer(this IServiceCollection services, string connectionString) {
            services.AddScoped<IDbService, DbService>(options => 
            new DbService(connectionString));
        }
    }
}
