using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath       = "protected.pdf";          // existing password‑protected PDF
        const string outputPath      = "updated.pdf";            // file to save with new credentials
        const string oldOwnerPassword = "ownerOld";              // current owner password (must be known)
        const string newUserPassword  = "userNew";               // desired new user password
        const string newOwnerPassword = "ownerNew";              // desired new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF using the current owner password.
            using (Document doc = new Document(inputPath, oldOwnerPassword))
            {
                // Change passwords: provide the current owner password, then the new user and owner passwords.
                doc.ChangePasswords(oldOwnerPassword, newUserPassword, newOwnerPassword);

                // Save the document with the updated credentials.
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