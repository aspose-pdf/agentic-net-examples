using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "ZUGFeRD_input.pdf";
        const string encryptedPath = "ZUGFeRD_encrypted.pdf";
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original ZUGFeRD PDF and encrypt it with both passwords.
            using (Document doc = new Document(inputPath))
            {
                // Define permissions you want to allow. Adjust as needed.
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ExtractContent |
                                    Permissions.ModifyContent; // Removed unavailable ModifyAnnotations flag

                // Encrypt using AES-256 (recommended) with user and owner passwords.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
