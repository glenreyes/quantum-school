using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Quantum.School.Core.Business;
using Quantum.School.Core.Models;
using Quantum.School.Core.Repository;

using Quantum.School.Infrastructure.Repository;

namespace Quantum.School.Api
{
	public class Startup
	{
		private const string AllowSpecificOrigins = "AllowSpecificOrigins";

		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// DB Context
			var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

			services
				.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(defaultConnectionString));

			var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
			services.AddCors(options =>
			{
				options.AddPolicy(AllowSpecificOrigins,
					builder =>
					{
						builder
							.WithOrigins(allowedOrigins)
							.AllowAnyHeader()
							.AllowAnyMethod()
							.WithExposedHeaders("WWW-Authenticate");
					});
			});

			services
				.AddRouting(options =>
				{
					options.LowercaseUrls = true;
				});

			services
				.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services
				.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantum.School.Api", Version = "v1" });
				});

			services
				.AddLogging(loggingBuilder =>
				{
					loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
					loggingBuilder.AddConsole();
					loggingBuilder.AddDebug();
				});

			//services.AddTransient<IEnrollmentManagement, EnrollmentManagement>();

			services.AddTransient<IClassScheduleRepository, ClassScheduleRepository>();
			services.AddTransient<ISubjectRepository, SubjectRepository>();
			services.AddTransient<IPersonRepository, PersonRepository>();
			services.AddTransient<ITeacherRepository, TeacherRepository>();
			services.AddTransient<IStudentRepository, StudentRepository>();

			services.AddTransient<IUserRepository, UserRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantum.School.Api v1"));
			}

			app.UseCors(AllowSpecificOrigins);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
