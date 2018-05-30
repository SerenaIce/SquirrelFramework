namespace SquirrelFramework.Utility.Common.Hash
{
    #region using directives

    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    #endregion

    /// <summary>
    /// Provide the Hash algorithm of the string
    /// </summary>
    /// <remarks>the name of hash algorithm, please visit to</remarks>
    /// <remarks>Url:</remarks>
    /// <see cref="http://msdn.microsoft.com/en-us/library/wet69s13(v=vs.85).aspx"/> for the valid names
    public static class HashCodeHelper
    {

        /// <summary>
        /// 获取MD5加密串
        /// </summary>
        /// <param name="contentText">待转换的内容</param>
        /// <returns>小写字母表示的MD5转换结果</returns>
        public static String GetMD5(String contentText)
        {
            if (String.IsNullOrEmpty(contentText)) { return String.Empty; }
            var md5 = new MD5CryptoServiceProvider();
            // 将字符编码为一个字节序列
            var data = System.Text.Encoding.Default.GetBytes(contentText);
            // 计算data字节数组的哈希值
            var md5Data = md5.ComputeHash(data);
            md5.Clear();
            return FormatStringMD5(md5Data);
        }

        /// <summary>
        /// 将MD5字节转换为字符串
        /// </summary>
        /// <param name="md5Data">待转换的MD5数据</param>
        /// <returns></returns>
        public static String FormatStringMD5(Byte[] md5Data)
        {
            //if (md5Data == null) return String.Empty;
            //var retMD5 = String.Empty;
            //for (var i = 0; i < md5Data.Length; i++)
            //{
            //    retMD5 += md5Data[i].ToString("x").PadLeft(2, '0');
            //}
            //return retMD5;
            return md5Data == null ?
                String.Empty : md5Data.Aggregate(String.Empty, (current, t) => current + t.ToString("x").PadLeft(2, '0'));
        }



        /// <summary>
        /// Returns a hash code for this string. The hash code for a
        /// String object is computed as
        /// 
        /// s[0]*31^(n-1) + s[1]*31^(n-2) + ... + s[n-1]
        /// 
        /// using int arithmetic, where s[i] is the
        /// i th character of the string, n is the length of
        /// the string, and ^ indicates exponentiation.
        /// (The hash value of the empty string is zero.)
        /// 
        ///  This extension method use the JAVA 5 String class hash code
        ///  algorithm to compute the JAVA hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns>a hash code value for this object</returns>
        public static Int32 ToJavaHashCode(String value)
        {
            var hashResult = default(Int32);
            if (!String.IsNullOrEmpty(value))
                Array.ForEach(value.ToCharArray(), item => hashResult = 31 * hashResult + item);
            return hashResult;
        }

        /// <summary>
        /// Get a string value's md5 hash and convert to a guid object
        /// </summary>
        /// <param name="value">value which will be compute hash</param>
        /// <returns>The result guid get from </returns>
        public static Guid StringHash(String value)
        {
            return new Guid(ToMD5HashCode(value));
        }

        /// <summary>
        /// Compute a MD5 hash value of the input string value
        /// </summary>
        /// <param name="value">input value</param>
        /// <returns>the result md5 of the input string value</returns>
        public static String ToMD5HashCode(String value)
        {
            return ToHashCode(value, "MD5");
        }

        /// <summary>
        /// Compute a hash value of the input string value using special hash algorithm
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="hashAlgorithmName">the name of hash algorithm, please visit to <remarks>Url:</remarks>
        /// </param>
        /// <see cref="http://msdn.microsoft.com/en-us/library/wet69s13(v=vs.85).aspx">Url for the valid names</see>
        /// <returns>the result hash code of the input string value</returns>
        public static String ToHashCode(String value, String hashAlgorithmName)
        {
            using (var hashAlgorithm = System.Security.Cryptography.HashAlgorithm.Create(hashAlgorithmName))
            {
                Debug.Assert(hashAlgorithm != null, "hashAlgorithm != null");
                hashAlgorithm.Initialize();
                var hashByteArray = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
                return BitConverter.ToString(hashByteArray).Replace("-", "").ToLowerInvariant();
            }
        }

        public static Byte[] ToHashSecretKey(String secretKey)
        {
            //Create a unsalted sha1 hash code
            var unsaltedSecretKey = CreateSha1Hash(secretKey);  //CreateSha512Hash(secretKey)

            //Generate a random salt key
            var rngProvider = new RNGCryptoServiceProvider();
            var saltValue = new Byte[SaltLength];
            rngProvider.GetBytes(saltValue);

            //Create salted secret key
            return CreateSaltedSecretKey(saltValue, unsaltedSecretKey); //CreateSha512SaltedSecretKey(saltValue, unsaltedSecretKey)
        }

        static Byte[] CreateSaltedSecretKey(Byte[] saltValue, Byte[] unsaltedSecretKey)
        {
            //The following is the main salted algorithm
            var rawSalted = new byte[unsaltedSecretKey.Length + saltValue.Length];
            unsaltedSecretKey.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedSecretKey.Length);

            var saltedSecretedKey = SHA1.Create().ComputeHash(rawSalted);
            var saltedSecretedKeyWithSaltArray = new byte[saltedSecretedKey.Length + saltValue.Length];
            saltedSecretedKey.CopyTo(saltedSecretedKeyWithSaltArray, 0);
            saltValue.CopyTo(saltedSecretedKeyWithSaltArray, saltedSecretedKey.Length);
            return saltedSecretedKeyWithSaltArray;
        }

        static Byte[] CreateSha512SaltedSecretKey(Byte[] saltValue, Byte[] unsaltedSecretKey)
        {
            var rawSalted = new byte[unsaltedSecretKey.Length + saltValue.Length];
            unsaltedSecretKey.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedSecretKey.Length);

            var saltedSecretedKey = SHA512.Create().ComputeHash(rawSalted);
            var saltedSecretedKeyWithSaltArray = new byte[saltedSecretedKey.Length + saltValue.Length];
            saltedSecretedKey.CopyTo(saltedSecretedKeyWithSaltArray, 0);
            saltValue.CopyTo(saltedSecretedKeyWithSaltArray, saltedSecretedKey.Length);
            return saltedSecretedKeyWithSaltArray;
        }

        const Int32 SaltLength = 4;
        public static Byte[] CreateSha1Hash(String secretKey)
        {
            return SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey)); 
        }

        public static Byte[] CreateSha512Hash(String secretKey)
        {
            return SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(secretKey)); 
        }

        public static Boolean IsTheSameSecretedKey(Byte[] saltedSecretKey, Byte[] unsaltedSecretKey)
        {
            if (saltedSecretKey == null || unsaltedSecretKey == null
               || unsaltedSecretKey.Length != saltedSecretKey.Length - SaltLength)
            {
                return false;
            }
            var saltValue = new Byte[SaltLength];
            Array.Copy(saltedSecretKey, saltedSecretKey.Length - SaltLength, saltValue, 0, SaltLength);
            var computedSaltedSecretKey = CreateSaltedSecretKey(saltValue, unsaltedSecretKey);
            return ComareByteArray(saltedSecretKey, computedSaltedSecretKey);
        }

        public static Boolean IsTheSameSha512SecretedKey(Byte[] saltedSecretKey, Byte[] unsaltedSecretKey)
        {
            if (saltedSecretKey == null || unsaltedSecretKey == null
               || unsaltedSecretKey.Length != saltedSecretKey.Length - SaltLength)
            {
                return false;
            }
            var saltValue = new Byte[SaltLength];
            Array.Copy(saltedSecretKey, saltedSecretKey.Length - SaltLength, saltValue, 0, SaltLength);
            var computedSaltedSecretKey = CreateSha512SaltedSecretKey(saltValue, unsaltedSecretKey);
            return ComareByteArray(saltedSecretKey, computedSaltedSecretKey);
        }

        static Boolean ComareByteArray(Byte[] saltedSecretKey, Byte[] computedSaltedSecretKey)
        {
            if (saltedSecretKey.Length != computedSaltedSecretKey.Length)
            {
                return false;
            }
            return !saltedSecretKey.Where((t, i) => t != computedSaltedSecretKey[i]).Any();
        }
    }
}