using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (no need to recreate it)
            Document pdfDoc = new Document(inputPath);

            // Define the permissions you want to allow (e.g., only printing)
            Permissions permissions = Permissions.PrintDocument;

            // Encrypt the document with 256‑bit AES encryption and set both user and owner passwords
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the protected PDF to a new file
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF encrypted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
