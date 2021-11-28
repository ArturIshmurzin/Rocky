using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Initializer;
using Rocky_DataAccess.Repository;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Utility;
using Rocky_Utility.BrainTree;
using System;

namespace Rocky
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBApplicationContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnextion")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<DBApplicationContext>();

            services.AddTransient<IEmailSender, MailSender>();

            services.AddHttpContextAccessor();
            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(60);
                Options.Cookie.IsEssential = true;
                Options.Cookie.HttpOnly = true;
            });

            services.Configure<BrainTreeSettings>(Configuration.GetSection("BrainTree"));
            services.AddSingleton<IBraneTreeGate, BrainTreeGate>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInquiryDetailRepository, InquiryDetailRepository>();
            services.AddScoped<IInquiryHeaderRepository, InquiryHeaderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IDBInitializer, DBInitializer>();

            services.AddAuthentication().AddFacebook(Options =>
            {
                Options.AppId = "235998958629958";
                Options.AppSecret = "fee8af2ff4cb7b7c9e67ab8a65580153";
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDBInitializer dBInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            dBInitializer.Initialize();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
