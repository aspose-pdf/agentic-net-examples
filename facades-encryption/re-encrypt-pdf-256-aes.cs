using System;
using System.IO;
using Aspose.Pdf;               // Document, Permissions, CryptoAlgorithm
// PdfFileSecurity is not used – Document handles encryption directly

class Program
{
    static void Main()
    {
        const string inputPath          = "encrypted_input.pdf";
        const string outputPath         = "reencrypted_output.pdf";
        const string currentUserPassword = "oldUserPwd";
        const string currentOwnerPassword = "oldOwnerPwd";
        const string newUserPassword     = "newUserPwd";
        const string newOwnerPassword    = "newOwnerPwd";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            using (Document doc = new Document(inputPath, currentUserPassword))
            {
                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply new 256‑bit AES encryption with the new passwords
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
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