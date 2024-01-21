using Microsoft.AspNetCore.Identity;
using SoccerPlus.Application.User.Model.Request;
using SoccerPlus.Application.User.Model.Response;
using SoccerPlus.Application.User.Services;
using SoccerPlus.Infra.Http.Authentication;
using SoccerPlus.Infra.Http.Authentication.Request;
using System.Security.Cryptography;
using System.Text;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IPasswordHasher<IdentityUser> _passwordHasher;
    private readonly IAuthenticateService _authenticationService;

    public UserService(
        IPasswordHasher<IdentityUser> passwordHasher,
        IAuthenticateService authenticationService,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _passwordHasher = passwordHasher;
        _authenticationService = authenticationService;
    }

    public async Task<string> AuthenticateAsync(UserRequest requestUser)
    {
        AuthenticationRequest auth = new AuthenticationRequest
        {
            Username = requestUser.Username,
            Password = requestUser.Password,
        };

        var token = await _authenticationService.Authenticate(auth);

        return token;
    }

    public async Task<UserCreatedResponse> RegisterAsync(UserRequest request)
    {
        var passwordHash = _passwordHasher.HashPassword(null, request.Password);
        IdentityResult created = new IdentityResult();
        UserCreatedResponse createdResponse = new UserCreatedResponse();

        var user = new IdentityUser
        {
            UserName = request.Username,
            NormalizedUserName = request.Username.ToUpper(),
            Email = request.Username,
            EmailConfirmed = true,
            PasswordHash = passwordHash,
            
        };
        try
        {
            created = await _userManager.CreateAsync(user);
            if (created.Succeeded)
            {
                createdResponse.Login = user.UserName;
                createdResponse.Password = passwordHash;

                await _signInManager.SignInAsync(user, false);
            }

        }
        catch (Exception e)
        {
            throw;
        }

        return createdResponse;
    }
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

}
