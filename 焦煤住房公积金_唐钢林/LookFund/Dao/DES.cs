using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace LookFund.Dao
{
    public class DES
    {
        public DES()
        {
        }
        public static byte[] DESKey = new byte[] { 0x84, 0xBC, 0xA1, 0x6A, 0xF5, 0x87, 0x3B, 0xE6 };
        public static byte[] DESIV = new byte[] { 0x62, 0x6A, 0x32, 0x64, 0x7F, 0x3A, 0x2A, 0xBB };
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="CryptText">加密字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string CryptText)
        {
            return DESEncrypt(CryptText, DESKey, DESIV);
        }

        public static string DESEncrypt(string CryptText, byte[] DESKey, byte[] DESIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] textOut = Encoding.Default.GetBytes(CryptText);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, des.CreateEncryptor(DESKey, DESIV), CryptoStreamMode.Write);
            CStream.Write(textOut, 0, textOut.Length);
            CStream.FlushFinalBlock();
            StringBuilder StrRes = new StringBuilder();
            foreach (byte Byte in MStream.ToArray())
            {
                StrRes.AppendFormat("{0:x2}", Byte);
            }
            return StrRes.ToString();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="CryptText">解密字符串</param>
        /// <returns></returns>
        public static string DESDecrypt(string CryptText)
        {
            return DESDecrypt(CryptText, DESKey, DESIV);
        }


        public static string DESDecrypt(string CryptText, byte[] DESKey, byte[] DESIV)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] textOut = new byte[CryptText.Length / 2];
                for (int Count = 0; Count < CryptText.Length; Count += 2)
                {
                    textOut[Count / 2] = (byte)(Convert.ToInt32(CryptText.Substring(Count, 2), 16));
                }
                MemoryStream MStream = new MemoryStream();
                CryptoStream CStream = new CryptoStream(MStream, des.CreateDecryptor(DESKey, DESIV), CryptoStreamMode.Write);
                CStream.Write(textOut, 0, textOut.Length);
                CStream.FlushFinalBlock();
                string str = System.Text.Encoding.Default.GetString(MStream.ToArray());
                return str;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Environment.Exit(-1);
                return null;
            }
        }
    }
}