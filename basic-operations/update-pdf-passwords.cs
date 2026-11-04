using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";
        const string outputPath     = "updated.pdf";
        const string oldUserPassword = "oldUserPass";
        const string ownerPassword   = "ownerPass";
        const string newUserPassword = "newUserPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the current user password
            using (Document doc = new Document(inputPath, oldUserPassword))
            {
                // Preserve desired permissions (adjust as needed)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with the new user password, keeping the same owner password
                doc.Encrypt(newUserPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the PDF with updated credentials
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