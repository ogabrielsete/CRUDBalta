using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TesteBalta.Data;
using TesteBalta.Mappings;
using TesteBalta.Repositorio;
using TesteBalta.Services;

namespace TesteBalta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "O campo é obrigatório.");
            });


            builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
                });




            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();

            // Função para configurar o dados requeridos na senha
            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 3;
            //    options.Password.RequiredUniqueChars = 1;
            //});


            builder.Services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
            builder.Services.AddScoped<IAssinaturasRepositorio, AssinaturaRepositorio>();
            builder.Services.AddHostedService<AtualizacaoAssinatura>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");


            app.Run();
        }
    }
}