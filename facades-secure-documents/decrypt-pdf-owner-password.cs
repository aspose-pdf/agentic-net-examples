using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with input and output file paths
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Decrypt using the owner password; returns true on success
                bool success = fileSecurity.DecryptFile(ownerPassword);
                if (success)
                {
                    Console.WriteLine($"Decryption succeeded. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Decryption failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}