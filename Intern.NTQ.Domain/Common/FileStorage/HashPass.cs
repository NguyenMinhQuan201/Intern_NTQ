using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Domain.Common.FileStorage
{

    public static class HashPass
    {
        public static string Hash(string passWord)
        {
            var sha = SHA256.Create();
            var asByteArr = Encoding.Default.GetBytes(passWord);
            var hashedPass = sha.ComputeHash(asByteArr);
            return Convert.ToBase64String(hashedPass);
        }

    }
}
