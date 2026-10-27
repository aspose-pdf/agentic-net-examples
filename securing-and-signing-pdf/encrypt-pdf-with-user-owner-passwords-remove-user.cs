using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string finalPath      = "final_no_user_password.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Step 1: Encrypt with both user and owner passwords
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }

            // Step 2: Open the encrypted file using the user password
            // and re‑encrypt with an empty user password (owner password stays)
            using (Document encDoc = new Document(encryptedPath, userPassword))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                // Empty string removes the user password while keeping encryption
                encDoc.Encrypt(string.Empty, ownerPassword, perms, CryptoAlgorithm.AESx256);
                encDoc.Save(finalPath);
            }

            Console.WriteLine("Encryption applied and user password removed successfully.");
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