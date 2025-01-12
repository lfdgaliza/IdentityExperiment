var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOpenIdConnectAccessTokenManagement();
// TODO: Client name should be in a constant
builder.Services.AddUserAccessTokenHttpClient("weather-forecast-api", configureClient: client => { 
    client.BaseAddress = new Uri("https://localhost:6001"); // TODO: appsettings.json
});

builder.Services
    .AddAuthentication(options =>
    {
        // We are using the oidc only to authenticate the user.
        options.DefaultChallengeScheme = "oidc";
        // After the user is authenticated, the auth token will be saved to a cookie and then we can use that
        options.DefaultScheme = "Cookies";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001"; // TODO: appsettings.json
        options.ClientId = "weather-web-app"; // TODO: appsettings.json
        options.ClientSecret = "my-super-secret"; // TODO: secret manager
        // This comes from /.well-known/openid-configuration (response_types_supported)
        // It should have a constant for that
        options.ResponseType = "code"; 

        // By default, the handler will request the "openid" and "profile" scopes.
        // We can be explicit by doing the follolwing:
        options.Scope.Clear();
        options.Scope.Add("openid"); // To authenticate the user
        options.Scope.Add("profile"); // To retrieve the user's profile
        options.Scope.Add("weather-forecast"); // To access the weather forecast API
        options.Scope.Add("offline_access"); // To retrieve refresh tokens

        // the "profile" scope claims are normally not included in the identity token to keep the token lean
        // Thanks to the standardization of the OIDC protocol, we can request the claims we need from the userinfo endpoint automatically
        options.GetClaimsFromUserInfoEndpoint = true;

        // Microsoft automatically rename some claims to use something they use internally.
        // I had issues with that in the past when trying to retrieve something
        // Let's disable that behavior.
        options.MapInboundClaims = false;

        // This is to avoid sending not needed details, like that we are using asp.net core.
        options.DisableTelemetry = true;

        // This will save the auth token to a cookie.
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
