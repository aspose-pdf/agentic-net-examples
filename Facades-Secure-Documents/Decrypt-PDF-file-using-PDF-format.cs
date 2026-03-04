using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity does not implement IDisposable, so we instantiate it directly.
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
        try
        {
            // DecryptFile returns true on success.
            bool success = security.DecryptFile(ownerPassword);
            if (success)
            {
                Console.WriteLine($"Decryption succeeded. Output saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Decryption failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during decryption: {ex.Message}");
        }
    }
}