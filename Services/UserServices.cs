using System;
using System.Collections.Generic;
using System.Linq;
using Test.Entities;
using Test.Helpers;

namespace Test.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.users.SingleOrDefault(x => x.Nickname == username);
            
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.users;
        }

        public User GetById(int id)
        {
            return _context.users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password es requerido");

            if (_context.users.Any(x => x.Nickname == user.Nickname))
                throw new AppException("Username \"" + user.Nickname + "\" no se encuentra disponible");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.users.Find(userParam.Id);
            if (user == null)
                throw new AppException("Usuario no encontrado");

            if (!string.IsNullOrWhiteSpace(userParam.Nickname) && userParam.Nickname != user.Nickname)
            {                
                if (_context.users.Any(x => x.Nickname == userParam.Nickname))
                    throw new AppException("Nickname " + userParam.Nickname + " no se encuentra disponible");

                user.Nickname = userParam.Nickname;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Name))
                user.Name = userParam.Name;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _context.users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.users.Find(id);
            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El valor no puede estar vacío o tener solo una cadena de espacios en blanco.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El valor no puede estar vacío o tener solo una cadena de espacios en blanco.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}