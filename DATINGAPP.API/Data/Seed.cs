using System.Collections.Generic;
using System.Linq;
using DATINGAPP.API.Models;
using Newtonsoft.Json;

namespace DATINGAPP.API.Data
{
    public class Seed
    {
        public DataContext _context { get; }
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach(var user in users) 
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                if(_context.Users.FirstOrDefault(x=>x.Username == user.Username) == null){
                _context.Users.Add(user);
                }
            }
            _context.SaveChanges();
        }
         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}