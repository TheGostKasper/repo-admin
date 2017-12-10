using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AMS.Helper
{
    public static class VXSecurity
    {
        private static string GetGSKey()
        {
            return ConfigurationManager.AppSettings["GSKey"];
        }
        public static string Encrypt(string input)
        {
            try
            {

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(GetGSKey()));
                //string key = SystemSettingsTools.GetGSKey();
                byte[] textAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
                input = System.Convert.ToBase64String(textAsBytes);
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = TDESKey;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Decrypt(string input)
        {
            try
            {
                input = input.Replace(" ", "+");
                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8Encoding.UTF8.GetBytes(GetGSKey()));
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = TDESKey;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                //return UTF8Encoding.UTF8.GetString(resultArray);
                input = UTF8Encoding.UTF8.GetString(resultArray);
                byte[] textAsBytes = System.Convert.FromBase64String(input);
                return System.Text.ASCIIEncoding.ASCII.GetString(textAsBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}