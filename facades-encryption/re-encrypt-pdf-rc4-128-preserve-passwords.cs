using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the encrypted PDF with the user password
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Preserve original permissions (cast from int to Permissions enum)
            Permissions perms = (Permissions)doc.Permissions;

            // Re‑encrypt using RC4 128‑bit algorithm while keeping the same passwords
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

            // Save the re‑encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
    }
}
