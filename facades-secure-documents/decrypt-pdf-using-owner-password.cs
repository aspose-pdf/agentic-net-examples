using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with the source (encrypted) and destination (decrypted) files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Decrypt the PDF using the owner password; returns true on success
                bool success = security.DecryptFile(ownerPassword);
                if (success)
                {
                    Console.WriteLine($"Decryption succeeded. Decrypted file saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine("Decryption failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}