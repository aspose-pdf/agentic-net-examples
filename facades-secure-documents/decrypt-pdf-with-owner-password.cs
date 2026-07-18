using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the encrypted source PDF and the decrypted output PDF
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";

        // Owner password used to unlock the PDF
        const string ownerPassword = "ownerpass";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileSecurity implements IDisposable, so wrap it in a using block
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Decrypt the PDF using the owner password.
                // DecryptFile returns true on success, otherwise false.
                bool result = fileSecurity.DecryptFile(ownerPassword);

                if (result)
                {
                    Console.WriteLine($"Decryption succeeded. Decrypted file saved as '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine("Decryption failed. Check the owner password and file integrity.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., I/O issues, invalid password format)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}