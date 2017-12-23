using Demo.SharedAuth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Using custom namespace here to ensure that our method is just "available" in startup.cs
/// </summary>
namespace Microsoft.Extensions.Configuration
{
    public static class ConfigureAuthExtension
    {
        public static void ConfigureOktaAuthentication(this IServiceCollection services, IConfiguration config)
        {
            //Get the configuration
            var myAuthSettings = config.GetSection("oktaSettings").Get<AuthSettings>();

            //Setup the high level authentication
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie().AddOpenIdConnect(options =>
            {
                options.ClientId = myAuthSettings.ClientId;
                options.ClientSecret = myAuthSettings.ClientSecret;
                options.Authority = myAuthSettings.Authority;
                options.CallbackPath = myAuthSettings.CallbackPath;
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.UseTokenLifetime = false;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                };
            });
        }
    }
}
