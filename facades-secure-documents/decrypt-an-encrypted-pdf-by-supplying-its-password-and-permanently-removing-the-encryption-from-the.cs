using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Path to the encrypted PDF
        const string outputPath = "decrypted.pdf"; // Path where the decrypted PDF will be saved
        const string password = "yourPassword";    // Owner or user password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Decrypt the PDF using the supplied password.
            // DecryptFile writes the decrypted document to outputPath.
            fileSecurity.DecryptFile(password);

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption failed: {ex.Message}");
        }
    }
}