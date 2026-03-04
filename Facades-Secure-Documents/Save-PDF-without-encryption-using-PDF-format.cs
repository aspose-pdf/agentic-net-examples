using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the encrypted source PDF and the decrypted output PDF
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Owner password of the encrypted PDF (use user password if no owner password)
        const string ownerPassword = "ownerpass";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfFileSecurity facade with source and destination file names
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Decrypt the PDF; returns true on success
            bool success = security.DecryptFile(ownerPassword);

            if (success)
            {
                Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Decryption failed.");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}