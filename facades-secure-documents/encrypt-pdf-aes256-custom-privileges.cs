using System;
using System.IO;
using Aspose.Pdf;

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
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Define custom permissions (allow printing and content extraction, deny modifications)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply AES‑256 encryption with the specified passwords and permissions
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Encryption successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
        }
    }
}
