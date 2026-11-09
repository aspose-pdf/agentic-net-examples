using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // No permissions – printing (and all other actions) are disabled
            Permissions permissions = (Permissions)0; // equivalent to "no permissions"

            // Apply password protection (AES‑256 encryption)
            doc.Encrypt(
                userPassword: "user123",
                ownerPassword: "admin456",
                permissions: permissions,
                cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password‑protected PDF saved to '{outputPath}'.");
    }
}
