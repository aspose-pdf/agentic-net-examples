using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "protected_input.pdf";
        const string outputPath = "updated_password.pdf";

        // Existing passwords
        const string currentOwnerPassword = "owner123";   // Owner password required to change passwords
        const string currentUserPassword  = "user123";   // Current user password (optional for opening)

        // New passwords to set
        const string newUserPassword  = "newUser456";
        const string newOwnerPassword = "newOwner456";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the password‑protected PDF using the owner password (or any valid password)
            using (Document doc = new Document(inputPath, currentOwnerPassword))
            {
                // Change passwords: first argument is the owner password,
                // followed by the new user password and the new owner password.
                doc.ChangePasswords(currentOwnerPassword, newUserPassword, newOwnerPassword);

                // Save the document with the updated credentials
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with new passwords to '{outputPath}'.");
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