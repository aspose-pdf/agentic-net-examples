using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "userZownerZ.pdf";
        const string userPassword = "userZ";
        const string ownerPassword = "ownerZ";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Forbid all permissions (no operations allowed)
                Permissions perms = (Permissions)0; // equivalent to no permissions

                // Encrypt with AES 128-bit algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

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
