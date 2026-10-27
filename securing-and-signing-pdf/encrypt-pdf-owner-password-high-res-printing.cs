using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum lives in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Owner password that protects the document; user password left empty to allow opening without a password
        const string ownerPassword = "OwnerSecret123";
        const string userPassword = ""; // no user password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF and apply encryption
            using (Document doc = new Document(inputPath))
            {
                // Allow printing (high‑resolution printing is the default when printing is allowed)
                // Do NOT grant copy/extract permission
                Permissions perms = Permissions.PrintDocument;

                // Encrypt using AES‑256 (recommended) with the specified passwords and permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"PDF encrypted successfully: {encryptedPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
