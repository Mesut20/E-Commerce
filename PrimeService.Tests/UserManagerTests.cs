using PrimeService;

[TestClass]
public sealed class UserManagerTests
{
    [TestMethod]
    public void ValidateUsername_ValidInput_ReturnsTrue()
    {
        var userManager = new UserManager();
        string username = "User123";
        
        bool result = userManager.ValidateUsername(username);
        
        Assert.IsTrue(result, "Valid Username Should Return True."); 
    }

    [TestMethod]
    public void ValidateUsername_TooShort_ReturnsFalse()
    {
        var userManager = new UserManager();
        string username = "t";
        
        bool result = userManager.ValidateUsername(username);
        
        Assert.IsFalse(result, "Username Is Too Short Should Return False.");
    }

    [TestMethod]
    public void ValidateUsername_TooLong_ReturnsFalse()
    {
        var userManager = new UserManager();
        Assert.IsFalse(userManager.ValidateUsername(new string('M', 40)), "Username Is Too Long Should Return False.");
    }

    [TestMethod]
    public void ValidateUsername_WithSpecialCharacter_ReturnsFalse()
    {
        var userManager = new UserManager();
        string username = "Mesut@123";
        
        bool result = userManager.ValidateUsername(username);
        
        Assert.IsFalse(result, "Username With Special Character Should Return False."); 
    }

    [TestMethod]
    public void ValidateUsername_Exactly5Characters_ReturnsTrue()
    {
        var userManager = new UserManager();
        Assert.IsTrue(userManager.ValidateUsername("Abcd1"), "Username With Exactly 5 Characters Should Return True.");
    }

    [TestMethod]
    public void ValidatePassword_ValidInput_ReturnsTrue()
    {
        var userManager = new UserManager();
        string password = "Password@123";
        
        bool result = userManager.ValidatePassword(password);
        
        Assert.IsTrue(result, "Valid Password Should Return True."); 
    }

    [TestMethod]
    public void ValidatePassword_WithoutSpecialCharacter_ReturnsFalse()
    {
        var userManager = new UserManager();
        string password = "Password123";
        
        bool result = userManager.ValidatePassword(password);
        
        Assert.IsFalse(result, "Password Without Special Character Should Return False."); 
    }

    [TestMethod]
    public void ValidatePassword_ShortPassword_ReturnsFalse()
    {
        var userManager = new UserManager();
        string password = "Pass@";
        
        bool result = userManager.ValidatePassword(password);
        
        Assert.IsFalse(result, "Password Shorter Than 8 Characters Should Return False."); 
    }

    [TestMethod]
    public void ValidateEmail_ValidInput_ReturnsTrue()
    {
        var userManager = new UserManager();
        string email = "user@gmail.com";
        
        bool result = userManager.ValidateEmail(email);
        
        Assert.IsTrue(result, "Valid Email Should Return True.");
    }

    [TestMethod]
    public void ValidateEmail_InvalidFormat_ReturnsFalse()
    {
        var userManager = new UserManager();
        string email = "userexample.com";
        
        bool result = userManager.ValidateEmail(email);
        
        Assert.IsFalse(result, "Email Without '@' Should Return False.");
    }

    [TestMethod]
    public void ValidateEmail_MissingDomain_ReturnsFalse()
    {
        var userManager = new UserManager();
        string email = "user@.com";
        
        bool result = userManager.ValidateEmail(email);
        
        Assert.IsFalse(result, "Email With Missing Domain Should Return False."); 
    }

    [TestMethod]
    public void ValidateEmail_UppercaseDomain_ReturnsTrue()
    {
        var userManager = new UserManager();
        string email = "user@example.com";
        
        bool result = userManager.ValidateEmail(email);
        
        Assert.IsTrue(result, "Email With Uppercase Domain Should Return True."); 
    }

    [TestMethod]
    public void RegisterUser_ValidInput_ReturnsSuccess()
    {
        var userManager = new UserManager();
        string Username = "mesut123";
        string Password = "Password@123";
        string email = "mesut.karadag@gmail.se";
        var result = userManager.Registeruser(Username, Password, email);
        Assert.IsTrue(result.IsSuccess, "Registration should succeed for valid input.");
        Assert.AreEqual($"User With {Username} Username Is Registered", result.message); 
    }

    [TestMethod]
    public void RegisterUser_DuplicateUsername_ReturnsFailure()
    {
        var userManager = new UserManager();
        string Username = "User1";
        userManager.Registeruser(Username, "Pass123!", "user@example.com");
        var result = userManager.Registeruser("User1", "Pass123!", "user@gmail.com");
        Assert.IsFalse(result.IsSuccess, "Registration Should Fail For Duplicate Username.");
        Assert.AreEqual($"Username {Username} Already Taken", result.ErrorMessage);
    }

    [TestMethod]
    public void RegisterUser_MultipleUniqueUsers_ReturnsSuccess()
    {
        var userManager = new UserManager();
        userManager.Registeruser("User1", "Pass123!", "user1@example.com");
        userManager.Registeruser("User2", "Pass@123", "user2@example.com");
        userManager.Registeruser("User3", "Pass@123", "user3@example.com");
        Assert.AreEqual(3, userManager.GetUserCount(), "User Counts"); 
    }

    [TestMethod]
    public void RegisterUser_StoreInList()
    {
        var userManager = new UserManager();
        var result = userManager.Registeruser("mesut123", "Password@123", "mesut.karadag@gmail.se");
        Assert.AreEqual(1, userManager.GetUserCount(), "Registered User Should Be Stored In The List."); 
    }
}