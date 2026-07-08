using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Path to the encrypted PDF
        const string outputPath = "decrypted.pdf"; // Path where the decrypted PDF will be saved
        const string ownerPassword = "ownerpass";  // Owner password for decryption

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with source and destination files
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Decrypt the PDF using the owner password.
                // DecryptFile throws an exception on failure; it also returns a bool indicating success.
                bool success = fileSecurity.DecryptFile(ownerPassword);

                if (success)
                {
                    Console.WriteLine($"Decryption succeeded. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine("Decryption failed (method returned false).");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the decryption process
            Console.Error.WriteLine($"Error during decryption: {ex.Message}");
        }
    }
}