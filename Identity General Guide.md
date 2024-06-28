1. Install all the dependencies and packages for Identity and Token handling:
    * `Microsoft.Extensions.Identity.Core`
    * `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
    * `Microsoft.AspNetCore.Authentication.JwtBearer`

2. Make a User model inheriting the IdentityUser model:

3. Inherit the DbContext from `IdentityDbContext<UserModel>`

4. Add the services into the builder and app in DI Controller.
    * add the identity authorization and authentication service 
```
    // adding identity service for user authentication
    builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
        options.Password.RequireDigit = 
        options.Password.RequireLowercase = 
        options.Password.RequireUppercase = 
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

    // adding authentication service with JWt
    builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = 
        options.DefaultChallengeScheme = 
        options.DefaultForbidScheme =
        options.DefaultScheme = 
        options.DefaultSignInScheme = 
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
        // add the token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Signingkey"])),

            ValidateLifetime = true, // Ensure token hasn't expired
            ClockSkew = TimeSpan.Zero // This will allow for a zero tolerance for the expiration of the token
        
        };
    });

    app.UseAuthorization();
    app.UseAuthentication();
```

5. Migrate the changes and update the database:

6. Configure the account controller and also configure the role by seeding on the dbcontext
    * `UserManager<UserModel>` has the functinality required to register and login the user, just have to register it on the constructor of the controller. Also remember to add the user into the Role after the successful creation of Users.

7. Generating `JWT Tokens`: 