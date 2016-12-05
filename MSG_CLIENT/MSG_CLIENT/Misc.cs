using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace MSG_CLIENT
{
    static class Misc
    {
        public static string RemoteIP()
        {
            return new WebClient().DownloadString(@"https://api.ipify.org").Trim();
        }

        public static Boolean HasConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static string SecureRandomString(int size)
        {
            byte[] bytes = new byte[size];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            rng.Dispose();
            return Convert.ToBase64String(bytes);
        }

        public static string TripleDES_Encrypt(this string textToEncrypt, SecureString _pass, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(textToEncrypt);
            AppSettingsReader settingsReader = new AppSettingsReader();
            byte[] k = UTF8Encoding.UTF8.GetBytes(Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(_pass)));
            List<byte> key = new List<byte>(k);
            if (key.Count < 24)
            {
                for (int diff = 24 - key.Count; diff > 0; diff--)
                    key.Add(new byte());
            }
            else if (key.Count > 24)
            {
                int diff = key.Count - 24;
                key.RemoveRange(24, diff);
            }
            if (useHashing == true)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(key.ToArray());
                hashmd5.Clear();
            }
            else { keyArray = key.ToArray(); }
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = keyArray;
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string TripleDES_Decrypt(this string textToDecrypt, SecureString _pass, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(textToDecrypt);
            AppSettingsReader settingReader = new AppSettingsReader();
            byte[] k = UTF8Encoding.UTF8.GetBytes(Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(_pass)));
            List<byte> key = new List<byte>(k);
            if (key.Count < 24)
            {
                for (int diff = 24 - key.Count; diff > 0; diff--)
                    key.Add(new byte());
            }
            else if (key.Count > 24)
            {
                int diff = key.Count - 24;
                key.RemoveRange(24, diff);
            }
            if (useHashing == true)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(key.ToArray());
                hashmd5.Clear();
            }
            else { keyArray = key.ToArray(); }
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = keyArray;
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");
            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }
        public static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");
            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }
        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            bounds.Width = bounds.Width - 1;
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;

        }
    }
}