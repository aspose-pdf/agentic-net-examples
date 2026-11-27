using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "reencrypted_output.pdf";
        const string oldUserPwd     = "oldUserPassword";
        const string oldOwnerPwd    = "oldOwnerPassword";
        const string newUserPwd     = "newUserPassword";
        const string newOwnerPwd    = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing encrypted PDF using the current user password
            using (Document doc = new Document(inputPath, oldUserPwd))
            {
                // Preserve desired permissions (adjust as needed)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with new passwords, using AES‑256 (CryptoAlgorithm.AESx256)
                doc.Encrypt(newUserPwd, newOwnerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the PDF with the updated passwords
                doc.Save(outputPath);
            }

            Console.WriteLine($"Passwords changed successfully. Saved to '{outputPath}'.");
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