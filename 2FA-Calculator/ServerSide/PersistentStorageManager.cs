namespace _2FA_Calculator.ServerSide
{
    class PersistentStorageManager
    {
        private string storageFilePath;
        Dictionary<string, Dictionary<string, string>>? dynamicStorage;

        public PersistentStorageManager(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
            this.dynamicStorage = PopulateDynamicStorage();
        }

        public Dictionary<string, Dictionary<string, string>>? PopulateDynamicStorage()
        {
            Dictionary<string, Dictionary<string, string>> result
                = new Dictionary<string, Dictionary<string, string>>();

            if (this.storageFilePath != null && result != null)
            {
                string? line = string.Empty;

                using (StreamReader sr = new StreamReader(storageFilePath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');

                        Dictionary<string, string> newDict = new Dictionary<string, string>();
                        if (newDict != null)
                        {
                            result[tokens[0]] = newDict;
                            result[tokens[0]].Add("hashed password", tokens[1]);
                            result[tokens[0]].Add("salt", tokens[2]);
                            result[tokens[0]].Add("auth", tokens[3]);
                            result[tokens[0]].Add("time when locked out", tokens[4]);
                        }
                    }
                }

                this.dynamicStorage = result;
                return result;
            }

            // Throw an exception *************************************************************************************************
            return null;
        }

        public bool SaveAllUsersCredentials()
        {
            if (this.storageFilePath != null && this.dynamicStorage != null)
            {
                using (StreamWriter sw = new StreamWriter(storageFilePath))
                {
                    foreach (string user in this.dynamicStorage.Keys)
                    {
                        sw.WriteLine(user + ","
                            + dynamicStorage[user]["hashed password"] + ","
                            + dynamicStorage[user]["salt"] + ","
                            + dynamicStorage[user]["auth"] + ","
                            + dynamicStorage[user]["time when locked out"]);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
