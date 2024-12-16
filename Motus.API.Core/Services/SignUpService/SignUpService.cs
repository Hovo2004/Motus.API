using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Motus.API.Data.DAO;
using Motus.API.Data.Entities;

namespace Motus.API.Core.Services.SignUpService
{
    public class SignUpService : ISignUpService
    {
        private MainDbContext MainDbContext;
        public SignUpService(MainDbContext maindbcontext) 
        {
            MainDbContext = maindbcontext;
        }

        private void Mail(string email)
        {
            try
            {

                // Email sender and credentials
                string senderEmail = "motusinarmenia@gmail.com";
                string senderPassword = "Motus.2024"; // Use environment variables for security

                // SMTP configuration
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) // Use your SMTP server and port
                {
                    Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                // Generate a confirmation link (mock example)
                //string confirmationToken = Guid.NewGuid().ToString(); // Unique token for user verification
                string confirmationLink = $"https://localhost:7245/ActivateUser?email={email}";

                // Email content
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "Motus"),
                    Subject = "Email Confirmation",
                    Body = $"<p>Hi,</p><p>Please confirm your email address by clicking the link below:</p>" +
                           $"<a href='{confirmationLink}'>Confirm Email</a>",
                    IsBodyHtml = true
                };

                // Add recipient
                mailMessage.To.Add(email);

                // Send the email
                smtpClient.Send(mailMessage);

                Console.WriteLine("Confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                // Consider logging the error in a real-world application
            }
        }
        public void AddUser(string firstname, string lastname, string email, DateTime data, string phonenumber, string password)
        {
            UserEntity userEntity = new UserEntity()
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                BirthDay = data,
                PhoneNumber = phonenumber,
                Password = password
            };
            MainDbContext.Users.Add(userEntity);
            MainDbContext.SaveChanges();
        }

        public void DeleteUser(string email)
        {

            UserEntity? DeleteUser = MainDbContext.Users.FirstOrDefault(User => User.Email == email);
            if (DeleteUser != null)
            {
                MainDbContext.Users.Remove(DeleteUser);
                MainDbContext.SaveChanges();
            }
        }

        public void UserActivation(string email)
        {
            Mail(email);
            UserEntity? ActivateUser = MainDbContext.Users.FirstOrDefault(User => User.Email == email);
            if (ActivateUser != null)
            {
                Mail(email);
                ActivateUser.IsActive = true;
                MainDbContext.Users.Update(ActivateUser);
                MainDbContext.SaveChanges();
            }
        }
    }
}
