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

            // Define permissions for the encrypted PDF
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the PDF using AES‑256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }
        catch (Exception ex)
        {
            // Log detailed exception information
            Console.Error.WriteLine("Encryption operation failed.");
            Console.Error.WriteLine($"Message: {ex.Message}");
            Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}