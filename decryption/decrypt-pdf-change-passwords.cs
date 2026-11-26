using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "changedAfterDecrypt.pdf";
        const string ownerPassword = "owner2";
        const string newUserPassword = "userChanged";
        const string newOwnerPassword = "ownerChanged";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the owner password.
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Decrypt the document (no parameters required).
                doc.Decrypt();

                // Define permissions for the re‑encrypted file.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with new passwords using AES‑256.
                doc.Encrypt(newUserPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the updated PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password changed and saved to '{outputPath}'.");
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