using System.Collections.Generic;
using System.Text;

namespace _2FA_Calculator.ServerSide
{
    class PersistentStorageManager
    {
        private string storageFilePath;

        public PersistentStorageManager(string storageFilePath)
        {
            this.storageFilePath = storageFilePath;
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
                            result[tokens[0]].Add("hashedPassword", tokens[1]);
                            result[tokens[0]].Add("salt", tokens[2]);
                            result[tokens[0]].Add("email", tokens[3]);
                        }
                    }
                }

                return result;
                
            }

            return null;
        }

        public bool SaveToPersistentStorage(Dictionary<string, Dictionary<string, string>> dynamicStorage)
        {
            if (this.storageFilePath != null)
            {
                using (StreamWriter sw = new StreamWriter(storageFilePath))
                {
                    foreach (string user in dynamicStorage.Keys)
                    {
                        sw.WriteLine(user + ","
                            + dynamicStorage[user]["hashedPassword"] + ","
                            + dynamicStorage[user]["salt"] + ","
                            + dynamicStorage[user]["email"]);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
