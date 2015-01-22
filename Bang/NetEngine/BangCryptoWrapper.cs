using System;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;

namespace Bang_.NetEngine
{
    class BangCryptoWrapper
    {
        #region Local variables
        private SymmetricAlgorithm symmetricAlg = null;
        private static int KEY_LEN = 32;            // in bytes
        private static string INIT_VECTOR = "@1B2c3D4e5F6g7H8";
        // Async pws exchange section parameters
        private static int RSA_KEY_LEN = 128;       // in bytes
        private RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(RSA_KEY_LEN*8);
        private string rsaOpenKey = null;
        #endregion

        #region C-tors
        public BangCryptoWrapper()
        {
            symmetricAlg = RijndaelManaged.Create();
            symmetricAlg.KeySize = KEY_LEN*8;
            symmetricAlg.Mode = CipherMode.CBC;
            symmetricAlg.IV = Encoding.ASCII.GetBytes(INIT_VECTOR);
            rsaProvider = new RSACryptoServiceProvider(RSA_KEY_LEN*8);
            GetPublicKey();
        }
        #endregion

        #region Helper functionality
        public byte[] GenerateRandomKey()
        {
            byte[] targetKey = new Byte[KEY_LEN];
            Random random = new Random((int)DateTime.Now.Ticks);
            random.NextBytes(targetKey);
            return targetKey;
        }
        #endregion

        #region Public functionality
        // Returns random-generated session key bytes
        public string GenerateEncryptedSessionKey(string openKey, byte[] sessionKey) 
        {
            string targetKeyData;
            try
            {
                // Obtain RSA params from XML serialized obj
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSAParameters));
                StringReader stringReader = new StringReader(openKey);
                RSAParameters rp = (RSAParameters)xmlSerializer.Deserialize(stringReader);
                // Generate & encrypt session pwd
                RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider(RSA_KEY_LEN * 8);
                rsa2.ImportParameters(rp);
                AsymmetricKeyExchangeFormatter kf = (AsymmetricKeyExchangeFormatter)new RSAOAEPKeyExchangeFormatter(rsa2);
                targetKeyData = Convert.ToBase64String(kf.CreateKeyExchange(sessionKey));
            }
            catch 
            {
                targetKeyData = "";
            }
            return targetKeyData;
        }

        // Obtains session key bytes from data encoded with RSA. Will be equal to "" in case of error
        public string GetSessionKey(string encKey)
        {
            string targetKeyData;
            try
            {
                byte[] encKeyBytes = Convert.FromBase64String(encKey);
                AsymmetricKeyExchangeDeformatter kd = (AsymmetricKeyExchangeDeformatter)new RSAOAEPKeyExchangeDeformatter(rsaProvider);
                targetKeyData = Convert.ToBase64String((kd.DecryptKeyExchange(encKeyBytes)));
            }
            catch
            {
                targetKeyData = "";
            }
            return targetKeyData;
        }

        // Encrypt data with password specifieds
        public string Encrypt(string plainText, string password)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            //byte[] plainTextBytes = Encoding.UTF32.GetBytes(plainText);
            symmetricAlg.Key = Convert.FromBase64String(password);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(
                memStream, 
                symmetricAlg.CreateEncryptor(), 
                CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] encryptedData = memStream.ToArray();
            memStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(encryptedData);
        }

        // Decrypt data with password specifieds
        public string Decrypt(string cyperText, string password)
        {
            try
            {
                byte[] cyperTextBytes = Convert.FromBase64String(cyperText);
                symmetricAlg.Key = Convert.FromBase64String(password);
                MemoryStream memStream = new MemoryStream(cyperTextBytes);
                CryptoStream cryptoStream = new CryptoStream(
                    memStream,
                    symmetricAlg.CreateDecryptor(),
                    CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cyperTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (System.Exception)
            {
                return cyperText;
            }
            
        }

        // Returns XML serialized RSA open key used in GetSessionKey
        public string GetPublicKey()
        {
            if (rsaOpenKey == null)
            {
                RSAParameters rp = rsaProvider.ExportParameters(false);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(RSAParameters));
                StringWriter stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, rp);
                rsaOpenKey = stringWriter.ToString();
            }
            return rsaOpenKey;
        }
        #endregion
    }
}
