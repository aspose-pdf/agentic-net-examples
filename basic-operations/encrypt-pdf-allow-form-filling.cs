using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum lives here in older versions

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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Allow only form filling – use a permission that exists in all supported versions.
                // The "ModifyContent" flag provides the ability to edit annotations/forms in older releases.
                Permissions perms = Permissions.ModifyContent;

                // Encrypt using AES‑256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
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
