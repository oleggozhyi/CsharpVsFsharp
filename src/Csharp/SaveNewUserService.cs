using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp
{
    public class Email
    {
        public static Email ValidateEmailImperative(string email)
        {
            if (email.Length > 100)
                throw new Exception("too long email");
            if (!email.Contains("@"))
                throw new Exception("Should have @");

            return new Email { ValidatedEmail = email };
        }

        public static IResult<Email, string> ValidateEmail(string email) =>
            email.Length > 100
                ? "too long email".ToFailureResult<Email, string>()
            : !email.Contains("@")
                ? "Should have @".ToFailureResult<Email, string>()
            : new Email { ValidatedEmail = email }.ToSuccess<Email, string>();

        public string ValidatedEmail { get; set; }
    }
    public class User
    {
        public static IResult<User, string> CreateUser(string name, Email email) =>
            new User { Id = null, Name = name, Email = email }.ToSuccess<User, string>();

        public static User CreateUserImperative(string name, Email email)
        {
            return new User { Id = null, Name = name, Email = email };
        }

        public string Name { get; set; }
        public Email Email { get; set; }
        public int? Id { get; set; }
    }
    public class Db
    {
        public static IResult<User, string> SaveUser(User user)
        {
            try
            {
                int generatedId = 42;
                Console.WriteLine("Saving to db..");
                return new User
                {
                    Email = user.Email,
                    Id = generatedId,
                    Name = user.Name
                }.ToSuccess<User, string>();
            }
            catch (Exception ex)
            {
                return ex.ToString().ToFailureResult<User, string>();
            }
        }

        public static User SaveUserImperative(User user)
        {
            int generatedId = 42;
            Console.WriteLine("Saving to db..");
            return new User
            {
                Email = user.Email,
                Id = generatedId,
                Name = user.Name
            };
        }
    }
    public class SaveNewUserService
    {
        public static IResult<User, string> SaveNewUser(string name, string email)
             => from validatedEmail in Email.ValidateEmail(email)
                from user in User.CreateUser(name, validatedEmail)
                from savedUser in Db.SaveUser(user)
                select savedUser;

        public static User SaveNewUserImperative(string name, string email)
        {
            var validatedEmail = Email.ValidateEmailImperative(email);
            var user = User.CreateUserImperative(name, validatedEmail);
            return Db.SaveUserImperative(user);
        }
    }
}
