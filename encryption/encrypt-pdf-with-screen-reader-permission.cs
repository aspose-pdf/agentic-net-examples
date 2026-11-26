using System;
using System.IO;
using Aspose.Pdf; // Document, CryptoAlgorithm
using Aspose.Pdf.Security; // Permissions enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Enable screen‑reader access via the appropriate permission flag
                Permissions perms = Permissions.ExtractContentWithDisabilities;

                // Encrypt with RC4 128‑bit algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"PDF encrypted successfully and saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
