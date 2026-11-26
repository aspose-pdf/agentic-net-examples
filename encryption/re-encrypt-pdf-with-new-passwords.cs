using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reEncrypted.pdf";
        const string oldUserPassword = "userOld";
        const string newUserPassword = "userNew";
        const string newOwnerPassword = "ownerNew";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF with the existing user password
            using (Document doc = new Document(inputPath, oldUserPassword))
            {
                // Set desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with new passwords using AES‑128
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx128);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}