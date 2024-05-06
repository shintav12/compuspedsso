using System.Security.Cryptography;

namespace CompuSPED.Service.Utils
{
    public static class HashPassword
    {
        public static byte[] CreateDBPassword(string password)
        {
            byte[] UnsaltedPassword = CreatePasswordHash(password);
            byte[] SaltValue = new byte[4];
            RNGCryptoServiceProvider Rng = new RNGCryptoServiceProvider();
            Rng.GetBytes(SaltValue);
            return CreateSaltedPassword(SaltValue, UnsaltedPassword);
        }

        private static byte[] CreatePasswordHash(string password)
        {
            SHA1Managed Sha1 = new SHA1Managed();
            return Sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static byte[] CreateSaltedPassword(byte[] saltValue, byte[] unsaltedPassword)
        {
            byte[] RawSalted = new byte[unsaltedPassword.Length + saltValue.Length - 1 + 1];
            unsaltedPassword.CopyTo(RawSalted, 0);
            saltValue.CopyTo(RawSalted, unsaltedPassword.Length);

            SHA1Managed Sha1 = new SHA1Managed();
            byte[] SaltedPassword = Sha1.ComputeHash(RawSalted);

            byte[] DbPassword = new byte[SaltedPassword.Length + saltValue.Length - 1 + 1];
            SaltedPassword.CopyTo(DbPassword, 0);
            saltValue.CopyTo(DbPassword, SaltedPassword.Length);

            return DbPassword;
        }
    }
}
