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
        // Owner password is intentionally left undefined (null)
        const string ownerPassword = null;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Define desired permissions (e.g., allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using only a user password; owner password is null
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF encrypted successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
