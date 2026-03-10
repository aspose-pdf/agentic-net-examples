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

        // Owner (or user) password required to open the encrypted document
        const string password = "ownerpass";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // CREATE: instantiate PdfFileSecurity with source and destination file names
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // LOAD & DECRYPT: attempt to decrypt using the provided password
            // DecryptFile returns true on success, false otherwise
            bool decrypted = fileSecurity.DecryptFile(password);

            if (decrypted)
            {
                Console.WriteLine($"Decryption succeeded. Decrypted file saved as '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Decryption failed. Check the password and try again.");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., file access issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}