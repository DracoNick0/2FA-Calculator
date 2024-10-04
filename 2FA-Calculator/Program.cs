using _2FA_Calculator.UserLogin;

UserAuthenticator userLogin = new UserAuthenticator();
userLogin.RequestUserAndPass();
Console.WriteLine("Username: " + userLogin.getUsername());
Console.WriteLine("Password: " + userLogin.getPassword());
