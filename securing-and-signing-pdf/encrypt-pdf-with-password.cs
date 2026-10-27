using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "open123";   // password required to open the file
        const string ownerPassword  = "edit123";   // password required to change permissions / edit

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF and encrypt it.
            using (Document doc = new Document(inputPath))
            {
                // Define permissions: allow printing and content extraction, but disallow modifications.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES‑256. The user password protects opening,
                // the owner password protects editing (changing permissions, removing encryption, etc.).
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}