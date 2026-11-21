using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Restrict all permissions (no printing, no editing, etc.)
            // Using a zero‑value cast because the library may not expose a Permissions.None member.
            Permissions perms = (Permissions)0;

            // Apply 128‑bit AES encryption with the specified passwords and permissions
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF encrypted with AES‑128 and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
