using BusinessLogicLayer.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestContactAPI.Tools
{
    public class TokenManager : ITokenManager
    {
        public static string secret = "OfOuYD2nzPUfL/WLz8JYDJIlhmVA8IbuO2o1vWzY8UOTG/gaVOaNNBar7hdX59USWfK7AzElt2cU+3JSNCrGRWOe/Vj169O1yRbMskpf1xAoDDSneLhmfYMQQRD+1WT66REh55hpdKsJuoFivlsIQtwN9Aq39H7ATI791QNI7RY=";
        public static string myIssuer = "monSiteApi.com";
        public static string myAudience = "monSite.com";

        public string GenerateJWT(UserClient user)
        {
            if (user.Email is null)
                throw new ArgumentNullException();

            //Création des crédentials
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Création de l'objet contenant les informations à stocker dans le token
            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            //Génération du token => Nuget : System.IdentityModel.Tokens.Jwt
            JwtSecurityToken token = new JwtSecurityToken(
                claims: myClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                issuer: myIssuer,
                audience: myAudience
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
