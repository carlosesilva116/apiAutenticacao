
using apiAutenticacao.Data;
using apiAutenticacao.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace apiAutenticacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Registrando os serviços de autentição e autorizão
            //Adicionando os serviços de autenticação usando o esquema de autenticação JWT Bearer, que é um padrão para autenticação baseada em tokens. Ele permite que os clientes obtenham um token JWT ao fazer login e, em seguida, usem esse token para acessar recursos protegidos na API.
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<AuthService>();

            //Configurando a autenticação JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options => {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //Valida o emissor do token
                        ValidateAudience = true, //Valida o destinatário do token
                        ValidateLifetime = true, //Valida a expiração do token
                        ValidateIssuerSigningKey = true, //Valida a chave de assinatura do token

                        ValidIssuer = builder.Configuration["Jwt:Issuer"], //Emissor válido do token
                        ValidAudience = builder.Configuration["Jwt:Audience"], //Destinatário válido do token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)) //Chave de assinatura do token

                    };
                        

                });
            //AddAuthorization é necessário para habilitar a autorização baseada em políticas, papéis ou outros requisitos. Ele permite que você defina regras de autorização para controlar o acesso a recursos específicos em sua aplicação, garantindo que apenas usuários autorizados possam acessar determinadas funcionalidades ou dados.
            builder.Services.AddAuthorization();

            // AddControllers é necessário para habilitar o suporte a controladores e ações em uma aplicação ASP.NET Core. Ele registra os serviços necessários para que os controladores possam ser resolvidos e processar as solicitações HTTP, permitindo que você defina rotas, manipule dados e retorne respostas adequadas aos clientes.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            //UseAuthentication() Adiciona o middleware de autenticação ao pipeline de processamento de solicitações. Ele é responsável por verificar as credenciais do usuário e estabelecer a identidade do usuário para a solicitação atual. Sem esse middleware, a autenticação não funcionará corretamente, e os usuários não poderão acessar recursos protegidos na API.
            //pipiline de processamento de requisições 
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
