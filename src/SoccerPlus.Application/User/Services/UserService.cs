using Microsoft.AspNetCore.Identity;
using SoccerPlus.Application.Common;
using SoccerPlus.Application.User.Model.Request;
using SoccerPlus.Application.User.Model.Response;
using SoccerPlus.Application.User.Services;
using SoccerPlus.Domain.SeedWork.Notification;
using SoccerPlus.Infra.Http.Authentication;
using SoccerPlus.Infra.Http.Authentication.Request;
using System.Security.Cryptography;
using System.Text;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IPasswordHasher<IdentityUser> _passwordHasher;
    private readonly IAuthenticateService _authenticationService;

    public UserService(
        IPasswordHasher<IdentityUser> passwordHasher,
        IAuthenticateService authenticationService,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        INotification notification
        ) : base(notification)
        
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
        IdentityResult created = new();
        IdentityUser userList = new();
        UserCreatedResponse createdUser = new();


        var user = new IdentityUser
        {
            UserName = request.Username,
            NormalizedUserName = request.Username?.ToUpper(),
            Email = request?.Username,
            EmailConfirmed = true,
            PasswordHash = passwordHash,
            
        };
        try
        {
            created = await _userManager.CreateAsync(user);

            if (created.Succeeded)
            {
                userList = _userManager.Users.Where(x => x.UserName == request.Username).FirstOrDefault();

                createdUser.Username = userList?.UserName;
                createdUser.Id = userList?.Id;
            }

        }
        catch (Exception e)
        {
            throw;
        }

        return createdUser;
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
