using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "protected_input.pdf";
        const string outputPath = "protected_output.pdf";

        // Existing owner password (must have owner rights to change passwords)
        const string currentOwnerPassword = "owner123";

        // New passwords to set
        const string newUserPassword = "newUser456";
        const string newOwnerPassword = "newOwner789";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the password‑protected PDF using the current owner password
            using (Document doc = new Document(inputPath, currentOwnerPassword))
            {
                // Change the passwords: provide the current owner password,
                // then the new user password and the new owner password.
                doc.ChangePasswords(currentOwnerPassword, newUserPassword, newOwnerPassword);

                // Save the PDF with the updated credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF passwords updated and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
