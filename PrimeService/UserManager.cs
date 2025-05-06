using System.Text.RegularExpressions;

namespace PrimeService;

public class UserManager
{
   
    private readonly List<User> _users = new();

    
    public int GetUserCount() => _users.Count;

    
    public RegistrationResult Registeruser(string username, string password, string email)
    {
        
        if (_users.Any(u => u.UserName == username))
        {
            return new RegistrationResult 
            {
                IsSuccess = false, 
                ErrorMessage = $"Username {username} already taken" 
            };
        }

        
        if (!ValidateUsername(username))
        {
            
            return new RegistrationResult
            {
                IsSuccess = false, 
                ErrorMessage = "Invalid Username"
            };
        }

        
        if (!ValidateEmail(email))
        {
            return new RegistrationResult 
            { 
                IsSuccess = false, 
                ErrorMessage = "Invalid Email"
            };
        }

        
        if (!ValidatePassword(password))
        {
            return new RegistrationResult 
            {
                IsSuccess = false, 
                ErrorMessage = "Invalid Password"
            };
        }

        
        _users.Add(new User
        {
            UserName = username,
            Password = password,
            Email = email
        });

        
        return new RegistrationResult
        {
            IsSuccess = true, 
            message = $"User with {username} username is Registered"
        };
    }

    
    public bool ValidateEmail(string email)
    {
        
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$"); 
        return regex.IsMatch(email); 
    }

    
    public bool ValidatePassword(string password)
    {
        
        if (password.Length < 8) 
        {
            return false;
        }
        return password.Any(c => !char.IsLetterOrDigit(c)); 
    }

    
    public bool ValidateUsername(string username)
    {
       
        if (username.Length < 5 || username.Length > 20) 
        {
            return false;
        }
        return username.All(c => char.IsLetterOrDigit(c)); 
    }
}