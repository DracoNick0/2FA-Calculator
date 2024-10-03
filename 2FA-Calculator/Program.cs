using _2FA_Calculator.UserLogin;

UserLoginClass userLogin = new UserLoginClass();
userLogin.RequestUserAndPass();
Console.WriteLine("Username: " + userLogin.getUsername());
Console.WriteLine("Password: " + userLogin.getPassword());
