using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath       = "protected.pdf";
        const string outputPath      = "protected_updated.pdf";
        const string ownerPassword   = "owner123";      // current owner password
        const string newUserPassword = "newUser456";    // new user password to set
        const string newOwnerPassword = "newOwner789"; // optional new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password (required for password change)
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Change passwords: provide current owner password, then new user and owner passwords
                doc.ChangePasswords(ownerPassword, newUserPassword, newOwnerPassword);

                // Save the PDF with the updated credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password updated and saved to '{outputPath}'.");
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