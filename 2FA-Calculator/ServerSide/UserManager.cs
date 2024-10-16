using System.Text;

namespace _2FA_Calculator.ServerSide
{
    class UserManager
    {
        private Hasher hasher;
        private string storageFilePath;

        public UserManager(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
            this.hasher = new Hasher();
        }

        public string getUserEmail(string username)
        {
            string? line = string.Empty;
            Hasher hasher = new Hasher();

            using (StreamReader sr = new StreamReader(storageFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    // Find line with the username and email, return email
                    if (tokens[0].CompareTo(username) == 0)
                    {
                        return tokens[3];
                    }
                }
            }

            return string.Empty;
        }

        public bool createAccount(string username, string password, string email)
        {
            if (!userExists(username))
            {
                int saltLength = 8;
                string salt = generateSalt(saltLength);
                string hashedPassword = hasher.computeSha256Hash(password + salt);

                using (StreamWriter sw = new StreamWriter(storageFilePath, true))
                {

                    string line = username + "," + hashedPassword + "," + salt + "," + email;
                    sw.WriteLine(line);
                    sw.Flush();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        // Storage of passwords needs work, also this needs to be broken up*******************************************************************************************************************
        public void updatePassword(string userOrEmail, string newPassword)
        {
            int credentialIndex = 0;

            if (RandomFunctions.isValidEmail(userOrEmail))
            {
                credentialIndex = 3;
            }
            else
            {
                credentialIndex = 0;
            }

            string? line = string.Empty;
            int lineToModify = -1;
            string[] tokens = new string[4];

            // Read through the user storage
            using (StreamReader sr = new StreamReader(storageFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    lineToModify++;
                    tokens = line.Split(',');

                    // Check if this line contains the username
                    if (tokens[credentialIndex].CompareTo(userOrEmail) == 0)
                    {
                        break;
                    }
                }
            }

            // Takes the whole storage file and edits one line.
            string[] wholeTextFile = File.ReadAllLines(storageFilePath);
            if (lineToModify < wholeTextFile.Length)
            {
                wholeTextFile[lineToModify] = tokens[0] + ","+ hasher.computeSha256Hash(newPassword + tokens[2]) + "," + tokens[2] + "," + tokens[3];
            }

            // Writes the edited version back to the storage file.
            File.WriteAllLines(storageFilePath, wholeTextFile);
        }

        public bool userExists(string username)
        {
            string? line = string.Empty;

            // Read through the user storage
            using (StreamReader sr = new StreamReader(storageFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');

                    // Check if this line contains the username
                    if (tokens[0].CompareTo(username) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string generateSalt(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
