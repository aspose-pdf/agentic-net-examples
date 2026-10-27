using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths and passwords
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Define permissions
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using AES‑256
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log detailed exception information
            Console.Error.WriteLine("Encryption failed.");
            Console.Error.WriteLine($"Message: {ex.Message}");
            Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}
