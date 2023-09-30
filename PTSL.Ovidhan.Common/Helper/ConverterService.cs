using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

using static System.Net.WebRequestMethods;

namespace PTSL.Ovidhan.Common.Helper
{
    public static class ConverterService
    {

        public static byte[] ObjectToByteArray<T>(this T obj)
        {
            if (obj == null)
            {
                return new byte[] { };
            }

            return JsonSerializer.SerializeToUtf8Bytes<T>(obj);
        }
        public static T ByteArrayToObject<T>(this byte[] bytes)
        {
            if (bytes.IsNullOrEmpty() || bytes.Length <= 0)
            {
                return default(T);
            }
            var jsonUtfReader = new Utf8JsonReader(bytes);
            return JsonSerializer.Deserialize<T>(ref jsonUtfReader);
        }


        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}

