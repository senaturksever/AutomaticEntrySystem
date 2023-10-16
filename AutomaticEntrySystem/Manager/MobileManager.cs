using AutomaticEntrySystem.Dtos;
using AutomaticEntrySystem.Dtos.LoginDto;
using AutomaticEntrySystem.Dtos.RegisterDto;
using AutomaticEntrySystem.Library;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutomaticEntrySystem.Manager
{
    public class MobileManager
    {
        private readonly JwtModel _jwtModel;
        public MobileManager(IOptions<JwtModel> jwtModel)
        {
            _jwtModel = jwtModel.Value;
            //value dememiz nedeni IOptions tipinde olması
        }
        public RegisterResponseDto Register(RegisterRequestDto requestDto)
        {
            var param = new SqlParameter[2];
            param[0] = Database.SetParameter("@user", System.Data.SqlDbType.NVarChar, 50, "Input", requestDto.UserName);
            param[1] = Database.SetParameter("@mail", System.Data.SqlDbType.NVarChar, 50, "Input", requestDto.Email);
            var que = @"SELECT [Email], [UserName] FROM [Users] WHERE [Email] = @mail AND [UserName] = @user";
            var dt = Database.GetDataTableParameter(que, param);
            if(dt.Rows.Count == 0)
            {
                var parameter = new SqlParameter[3];
                parameter[0] = Database.SetParameter("@email", System.Data.SqlDbType.NVarChar, 50, "Input", requestDto.Email);
                parameter[1] = Database.SetParameter("@password", System.Data.SqlDbType.NVarChar, 50, "Input", requestDto.Password);
                parameter[2] = Database.SetParameter("@userName", System.Data.SqlDbType.NVarChar, 50, "Input", requestDto.UserName);

                var query = @"INSERT INTO [Users]
                                   ([Email]
                                   ,[Password]
                                   ,[UserName])
                             VALUES
                                   (@email
                                   ,@password
                                   ,@userName)";
                var result = Database.ExecuteNonQueryWithParameters(query, parameter);
                if (result > 0)
                {
                    return new RegisterResponseDto
                    {
                        Status = true,
                        statusCode = 1,
                        StatusMessage = "Kayıt işlemi başarılı"
                    };
                }
                return new RegisterResponseDto
                {
                    Status = false,
                    statusCode = 0,
                    StatusMessage = "Kayıt işlemi Başarısız"
                };
            }
            else
            {
                return new RegisterResponseDto
                {
                    Status = false,
                    statusCode = 0,
                    StatusMessage = "Tekrar Eden Kayıt"
                };
            }
          
        }

        public LoginResponseDto Login(LoginRequestDto loginRequestDto)
        {
            var param = new SqlParameter[2];
            param[0] = Database.SetParameter("@email", System.Data.SqlDbType.NVarChar, 50, "Input", loginRequestDto.Email);
            param[1] = Database.SetParameter("@password", System.Data.SqlDbType.NVarChar, 50, "Input", loginRequestDto.Password);

            string query = @"SELECT * FROM [Users] WHERE [Email] = @email AND [Password] = @password";
            if (!string.IsNullOrEmpty(query))
            {
                var token = CreateToken(loginRequestDto);
                return new LoginResponseDto { Status = true,AuthToken = token,StatusMessage ="Token oluşturuldu, giriş başarılı" };

            }
            return new LoginResponseDto { Status = false, StatusMessage = "Giriş Başarısız" };
        }
        public string CreateToken(LoginRequestDto loginRequest)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,loginRequest.Email)
            };
            var token = new JwtSecurityToken(_jwtModel.Issuer,
                _jwtModel.Audience,
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
