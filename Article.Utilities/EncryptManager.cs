using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Utilities
{
    public static class EncryptManager
    {
        public static string Base64Encrypt(string data)
        {
            try
            {
                byte[] dataByteArray = Encoding.ASCII.GetBytes(data);
                string encryptedData = Convert.ToBase64String(dataByteArray);
                return encryptedData;
            }
            catch (Exception)
            {
                return data;
            }
        }

        public static string Base64Decrypt(string descryptData)
        {
            try
            {
                byte[] descryptDataArray = Convert.FromBase64String(descryptData);
                string originalData = Encoding.ASCII.GetString(descryptDataArray);
                return originalData;
            }
            catch (Exception)
            {
                return descryptData;
            }
        }
    }
}
