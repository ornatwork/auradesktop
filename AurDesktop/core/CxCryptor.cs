//
using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Text;
using System.IO;
//


namespace org.auroracoin.desktop.core
{

    // Encrypts and descrypts strings
    public class CxCryptStr
    {
        // shhh  don't tell, it's a secret
        private const string KEY_SEED = "AZOS98567432100985674321";
        private const string VECTOR = "zzAlerts";
        // Get ready to crypt
        private static TripleDESCryptoServiceProvider mCrypto = new TripleDESCryptoServiceProvider();


        static CxCryptStr()
        {
            mCrypto.IV = Encoding.UTF8.GetBytes(VECTOR);
            mCrypto.Key = Encoding.UTF8.GetBytes(KEY_SEED);
        }

        /// <summary>
        /// Encrypts text, generates the init vector portion of the key
        /// </summary>
        /// <param name="psTheText">The text to be encrypted, which will be returned in its encrypted format</param>
        /// <returns>The init vector portion of the key</returns>
        internal static string encryptText(string psText)
        {
            return transform(psText, mCrypto.CreateEncryptor());
        }


        /// <summary>
        /// Decrypts encrypted text
        /// </summary>
        /// <param name="psEncryptedText">The encrypted text</param>
        /// <param name="psIV">The init vector portion of the key</param>
        /// <returns></returns>
        internal static string decryptText(string psText)
        {
            return transform(psText, mCrypto.CreateDecryptor());
        }


        private static string transform(string psText,
                                ICryptoTransform CryptoTransform)
        {
            MemoryStream stream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(stream, CryptoTransform, CryptoStreamMode.Write);

            byte[] input = Encoding.Default.GetBytes(psText);

            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();

            return Encoding.Default.GetString(stream.ToArray());
        }



    }  // EOC
}
