using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted2.pdf";
        const string userPassword = "user2";
        const string ownerPassword = "owner2";

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
                // Forbid all privileges by granting no permissions (zero flag value)
                Permissions noPermissions = (Permissions)0;

                // Encrypt using RC4 128‑bit algorithm
                doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

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
