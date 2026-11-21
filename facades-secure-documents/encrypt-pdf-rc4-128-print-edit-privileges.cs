using System;
using System.IO;
using Aspose.Pdf;

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

            // Combine privileges: allow printing and editing (modify content)
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;

            // Encrypt using RC4‑128 algorithm
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

            // Save the encrypted PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
