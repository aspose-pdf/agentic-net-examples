using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";   // password required to open the PDF
        const string ownerPassword  = "owner123";  // password required to change permissions / edit

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Define permissions for the user password (e.g., allow printing only)
                Permissions permissions = Permissions.PrintDocument;

                // Encrypt the document using AES‑256 algorithm
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}