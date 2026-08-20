using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "protected.pdf";
        const string outputPath = "protected_updated.pdf";

        // Current owner password (must be known to change passwords)
        const string currentOwnerPassword = "owner123";

        // New passwords to set
        const string newUserPassword  = "newUser456";
        const string newOwnerPassword = "newOwner789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password
            using (Document doc = new Document(inputPath, currentOwnerPassword))
            {
                // Change the user and owner passwords
                doc.ChangePasswords(currentOwnerPassword, newUserPassword, newOwnerPassword);

                // Save the PDF with the updated credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password updated successfully. Saved to '{outputPath}'.");
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