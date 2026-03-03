using apiAutenticacao.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apiAutenticacao.Services
{
    public class TokenService
    {
        //Injeção de dependência para acessar os valores do arquivo appsettings.json
        private readonly IConfiguration _configuration;

        //Contrutor para receber a instâcia de IConfiguration, que é usada para acessar os valores do arquivo appsettings.json
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Cria um método para gerar o token JWT
        public string GenerateToken(Usuario User) {

            //Buscamos os valores do arquivo appsettings.json para configurar o token JWT
            string Key = _configuration["Jwt:Key"]!;
            string Issuer = _configuration["Jwt:Issuer"]!;
            string Audience = _configuration["Jwt:Audience"]!;
            int durationInHours = int.Parse(_configuration["Jwt:DurationInMinutes"]!);

            //Converte a chave secreta em bytes para ser usada na geração do token
            var byTesKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Key));

            //Cria as credenciais de assinatura usando a chave secreta e o algoritmo de hash HMAC SHA256
            var credentials = new SigningCredentials(byTesKey, SecurityAlgorithms.HmacSha256);


            //Criamos as claims do token JWT, que são as informações que queremos incluir no token. Neste exemplo, estamos incluindo o email e o nome do usuário, além de um identificador único (Jti) para evitar tokens duplicados.
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, User.Email ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Criamos o token JWT usando as informações coletadas
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(durationInHours),
                signingCredentials: credentials
            );

            //Converte o token JWT para uma string e retorna para o cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
