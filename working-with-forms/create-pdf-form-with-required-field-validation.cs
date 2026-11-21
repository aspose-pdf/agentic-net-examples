using System;

namespace FormValidationApp
{
    public class FormValidation
    {
        /// <summary>
        /// Called after client‑side validation succeeds.
        /// </summary>
        /// <param name="name">The name entered in the form.</param>
        /// <param name="email">The e‑mail entered in the form.</param>
        public void ProcessSubmission(string name, string email)
        {
            // Server‑side processing after the form is successfully submitted.
            // Example: add your business logic here (e.g., save to database, send email, etc.)
            // For demonstration we simply write the values to the console.
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Email: {email}");
        }
    }

    public static class Program
    {
        /// <summary>
        /// Entry point required for a console‑style project.
        /// </summary>
        public static void Main(string[] args)
        {
            // In a real web scenario the values would be supplied by the client after JavaScript validation.
            // Here we provide simple defaults or use command‑line arguments for a quick demo.
            var validator = new FormValidation();

            string name = args.Length > 0 ? args[0] : "John Doe";
            string email = args.Length > 1 ? args[1] : "john.doe@example.com";

            validator.ProcessSubmission(name, email);
        }
    }
}