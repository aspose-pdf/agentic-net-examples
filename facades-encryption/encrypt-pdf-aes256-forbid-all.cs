using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Required for Permissions and CryptoAlgorithm enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF.
            Document doc = new Document(inputPath);

            // Encrypt with AES‑256, user/owner passwords, and forbid all permissions.
            // (Permissions)0 represents "no permissions" (equivalent to ForbidAll).
            doc.Encrypt(userPassword, ownerPassword, (Permissions)0, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF.
            doc.Save(outputPath);

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
