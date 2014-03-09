//
#if DEBUG
//
using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Text;
//
using NUnit.Framework;



namespace org.auroracoin.desktop.core
{
    
    [TestFixture]
    public class TxCrypt
    {
        // The crypter
        //private CxCryptStr mxCrypt = new CxCryptStr();
        private const string ROOT_LOCATION = @"Software";
        private const string SUB1_LOCATION = @"aurFam";
        private const string SUB2_LOCATION = @"locker";
        private const string VECTOR_LOCATION = "zabcdefgz_iv";

        [Test]
        public void twiceEncryptDecrypt()
        {
            // Encrypt and keep the IV 
            string sTextCrypt = "Cryptic message from here to there.";
            string sTextCrypt2 = "Cryptic message from here to somwhere else.";
            string sTextToCryptOriginal = (string)sTextCrypt.Clone();
            string sTextToCryptOriginal2 = (string)sTextCrypt2.Clone();
            string one = CxCryptStr.encryptText(sTextCrypt);
            Console.WriteLine("encrypted=" + one);
            string two = CxCryptStr.encryptText(sTextCrypt2);
            Console.WriteLine("encrypted=" + two);
            Console.WriteLine();


            // Let's decrypt
            string compare = CxCryptStr.decryptText(one);
            Console.WriteLine("decrypted=" + compare);
            Assert.AreEqual(sTextToCryptOriginal, compare);
            // Let's decrypt
            compare = CxCryptStr.decryptText(two);
            Console.WriteLine("decrypted=" + compare);
            Assert.AreEqual(sTextToCryptOriginal2, compare);
        }




     
    }  // EOC

}
#endif
