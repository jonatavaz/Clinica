using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Clinica
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona serviços para o MVC
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Mostra a página de erro de desenvolvimento
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Mostra a página de erro genérica em produção
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
            app.UseStaticFiles(); // Habilita arquivos estáticos (CSS, JS, imagens)

            app.UseRouting(); // Habilita o roteamento

            app.UseAuthorization(); // Habilita a autorização

            app.UseEndpoints(endpoints =>
            {
                // Define a rota padrão para o controlador Consultas
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Consultas}/{action=Index}/{id?}");
            });
        }
    }
}
