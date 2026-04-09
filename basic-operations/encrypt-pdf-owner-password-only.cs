using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string ownerPassword = "owner123";
        // Empty user password – users can open the file but have no permissions.
        const string userPassword = "";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // No permissions for the user (value 0) – restrict all actions.
                Permissions userPermissions = (Permissions)0;

                // Encrypt: empty user password, owner password, no user permissions, AES‑256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, userPermissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}