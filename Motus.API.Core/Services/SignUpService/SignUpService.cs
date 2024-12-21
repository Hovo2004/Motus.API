using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
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
                UserEntity? user = MainDbContext.Users.FirstOrDefault(u => u.Email == email);

                // Email sender and credentials
                string senderEmail = "motusinarmenia@gmail.com";
                string senderPassword = "upyp kbtg asak djgp"; // Use environment variables for security

                // SMTP configuration
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) // Use your SMTP server and port
                {
                    Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                string encodedEmail = WebUtility.UrlEncode(email);

                // Generate a confirmation link (mock example)
                string confirmationToken = Guid.NewGuid().ToString();  // Generate a unique token
                string encodedToken = WebUtility.UrlEncode(confirmationToken);

                string confirmationLink = $"https://localhost:7245/ActivateUser?email={encodedEmail}&token={encodedToken}";
                if (user != null)
                {
                    // Token is valid, activate the user
                    user.ConfirmationToken = confirmationToken;
                    MainDbContext.Users.Update(user);
                    MainDbContext.SaveChanges();
                }
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
        public bool AddUser(string firstname, string lastname, string email, DateTime data, string phonenumber, string password)
        {
            try
            {
                UserEntity? user = MainDbContext.Users.FirstOrDefault(u => u.Email == email);

                if (user != null) 
                {
                    Console.WriteLine("This email is existing");    
                    return false;
                }
                else
                {
                    // Create a new user entity
                    UserEntity userEntity = new UserEntity()
                    {
                        Firstname = firstname,
                        Lastname = lastname,
                        Email = email,
                        BirthDay = data, //birthDate, // Use the parsed date
                        PhoneNumber = phonenumber,
                        Password = password,
                        ConfirmationToken = null,
                        IsActive = false
                    };

                    // Add the user to the database
                    MainDbContext.Users.Add(userEntity);
                    MainDbContext.SaveChanges();
                    Mail(email);
                    Console.WriteLine("User added successfully.");
                    return true;
                }
                
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid date format. Please provide the date in 'YYYY/MM/DD' format.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                // Log the exception in a real-world application
            }
            return false;
        }
        public bool login(string email, string password) 
        {
            UserEntity? user = MainDbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("No such user");
                return false;
            }
            if (password == user.Password)
            {
                Console.WriteLine("Log in successfully");
                return true;
            }
            Console.WriteLine("Password is incorrect");
            return false;
        }
        public bool DeleteUser(string email)
        {
            try
            {
                // Check if the email is null or empty before querying
                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Error: Email is null or empty.");
                    return false;
                }

                // Perform a case-insensitive search for the user
                UserEntity? DeleteUser = MainDbContext.Users.FirstOrDefault(User => User.Email.ToLower() == email.ToLower());

                if (DeleteUser == null)
                {
                    Console.WriteLine($"No user found with email: {email}");
                    return false;
                }

                // Log the user's details to ensure correct retrieval
                Console.WriteLine($"Found user: {DeleteUser.Firstname} {DeleteUser.Lastname} ({DeleteUser.Email})");

                // Remove the user
                MainDbContext.Users.Remove(DeleteUser);
                MainDbContext.SaveChanges();

                Console.WriteLine($"User with email {email} deleted successfully.");
                return true;
            }
            catch (InvalidCastException ex)
            {
                // Specific catch for casting issues
                Console.WriteLine($"Casting error: {ex.Message}");
                // Log the exception (use a logging framework in a production environment)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                // Log the exception in a real-world application
            }
            return false;
        }

        public void UserActivation(string email, string token)
        {
            UserEntity? user = MainDbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && user.ConfirmationToken == token)
            {
                // Token is valid, activate the user
                user.IsActive = true;
                user.ConfirmationToken = null;  // Remove the token after successful activation
                MainDbContext.Users.Update(user);
                MainDbContext.SaveChanges();
                Console.WriteLine("User successfully activated.");
            }
            else
            {
                Console.WriteLine("Invalid token or user not found.");
            }
        }
    }
}
