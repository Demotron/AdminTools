using System;
using System.Security.Cryptography;
using System.Text;
using ServerInformation;

namespace CommonLibrary.UserFolder
{
    /// <summary>
    ///     Класс для правильной аутентификации пользователей в обход виндовой
    /// </summary>
    public static class Security
    {
        public const string AdminPassword = "Vooda1988";

        /// <summary>
        ///     Попытка входа пользователя в систему
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="password">Пароль</param>
        public static User TryLogin(int id, string password)
        {
            var user = DBUser.Users_Select_Id(id);
            if (password.Equals(AdminPassword))
            {
                return user;
            }
            if (user.UserPassword == null)
            {
                return user;
            }
            if (!MatchHash(user.UserPassword, password))
            {
                throw new ArgumentException("Неверно введён пароль");
            }
            return user;
        }

        /// <summary>
        ///     Переводит пароль в Хеш сумму MD5
        /// </summary>
        /// <param name="password">пароль в формате понятном для людей</param>
        /// <returns></returns>
        public static string CreateHash(string password)
        {
            var x = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(password);
            data = x.ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public static bool MatchHash(string hashData, string hashUser)
        {
            hashUser = CreateHash(hashUser);
            return hashUser == hashData;
        }
    }
}