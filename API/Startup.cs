using Microsoft.EntityFrameworkCore;
using MyVotingSystem.Accessors;
using MyVotingSystem.Data;

namespace MyVotingSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddDbContext<ApplicationDbContext>
                (options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IVoteAccessor, VoteAccessor>();
            services.AddScoped<IElectionAccessor, ElectionAccessor>();
            services.AddScoped<IBallotAccessor, BallotAccessor>();
            services.AddScoped<ICandidateAccessor, CandidateAccessor>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            {

            }
        }
    }
}