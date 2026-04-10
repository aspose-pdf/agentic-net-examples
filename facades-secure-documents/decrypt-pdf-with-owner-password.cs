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

        // Owner password used for decryption
        const string ownerPassword = "ownerpass";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with input and output file names
            // This follows the provided creation pattern for PdfFileSecurity.
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Decrypt the PDF using the owner password.
                // DecryptFile returns true on success.
                bool result = fileSecurity.DecryptFile(ownerPassword);

                Console.WriteLine(result
                    ? $"Decryption succeeded. Decrypted file saved to '{outputPath}'."
                    : "Decryption failed.");
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}