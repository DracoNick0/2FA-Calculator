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

        public Dictionary<string, Dictionary<string, string>>? populateDynamicStorage()
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

        /*
        public bool populatePersistentStorage(Dictionary<string, Dictionary<string, string>> dynamicStorage)
        {

            wholeTextFile[lineToModify] = tokens[0] + "," + hasher.computeSha256Hash(newPassword + tokens[2]) + "," + tokens[2] + "," + tokens[3];

            // Writes the edited version back to the storage file.
            File.WriteAllLines(storageFilePath, wholeTextFile);
            return true;
        }
        */
    }
}
