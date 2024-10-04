using _2FA_Calculator.UserLogin;

UserAuthenticationClass userLogin = new UserAuthenticationClass();
userLogin.RequestUserAndPass();
Console.WriteLine("Username: " + userLogin.getUsername());
Console.WriteLine("Password: " + userLogin.getPassword());
