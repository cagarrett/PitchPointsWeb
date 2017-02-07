using System;
using System.Linq;
using System.Security.Cryptography;

namespace PitchPointsWeb.Models
{

    /// <summary>
    /// Represents a user that is used in the API during registration
    /// </summary>
    public class RegisterAPIUser
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Password { get; set; }

    }

    /// <summary>
    /// Represents a user that is used in the API during logging in
    /// </summary>
    public class LoginAPIUser
    {

        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class User : UpdateableData
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool EmailVerified { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateRegistered { get; set; }

        public byte[] PasswordHash { get; internal set; }

        public byte[] Salt { get; internal set; }

        /// <summary>
        /// Updates this users password by updating both the salt and passwordHash
        /// </summary>
        /// <param name="password">The new password to use for this user</param>
        public void UpdatePassword(string password)
        {
            Salt = UserUtils.GenerateSalt();
            PasswordHash = UserUtils.HashPassword(password, Salt);
        }

        /// <summary>
        /// Determines if this password matches this users current password hash
        /// </summary>
        /// <param name="password">The password to check against the current password hash</param>
        /// <returns>True if this new password + current salt == current PasswordHash</returns>
        public bool PasswordsMatch(string password)
        {
            return UserUtils.DoPasswordsMatch(password, PasswordHash, Salt);
        }

        public static User CreateFrom(RegisterAPIUser user)
        {
            User normalUser = new User();
            normalUser.Email = user.Email;
            normalUser.DateOfBirth = user.DateOfBirth;
            normalUser.FirstName = user.FirstName;
            normalUser.LastName = user.LastName;
            normalUser.UpdatePassword(user.Password);
            normalUser.DateRegistered = DateTime.Now;
            return normalUser;
        }

    }

    public class UserUtils
    {

        private static readonly int MAX_SALT_LENGTH = 16;

        private static readonly int PASSWORD_ITERATIONS = 1024;

        /// <summary>
        /// Generates a random salt of max length supported in the User table database
        /// </summary>
        /// <returns>A random salt byte array</returns>
        public static byte[] GenerateSalt()
        {
            var salt = new byte[MAX_SALT_LENGTH];
            using (var command = new RNGCryptoServiceProvider())
            {
                command.GetNonZeroBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Hashes the password and salt and returns a byte array of 20 length
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The securely generated salt</param>
        /// <returns>A byte[] that represents the password and salt hash from Rfc2898</returns>
        public static byte[] HashPassword(string password, byte[] salt)
        {
            byte[] hash;
            using (var crypto = new Rfc2898DeriveBytes(password, salt, PASSWORD_ITERATIONS))
            {
                hash = crypto.GetBytes(20);
            }
            return hash;
        }

        /// <summary>
        /// Determines if this password matches the previous password and salt combination
        /// </summary>
        /// <param name="password">The new password to check against the currentHash</param>
        /// <param name="currentHash">The current password hash of a password and salt</param>
        /// <param name="salt">The securely generated salt that was used to hash the password that generated currentHash</param>
        /// <returns>True if password + salt == currentHash</returns>
        public static bool DoPasswordsMatch(string password, byte[] currentHash, byte[] salt)
        {
            byte[] newHash = HashPassword(password, salt);
            return Enumerable.SequenceEqual(newHash, currentHash);
        }

    }

}