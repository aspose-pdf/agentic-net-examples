using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade that handles PDF security operations
            PdfFileSecurity security = new PdfFileSecurity();

            // Bind the encrypted PDF file to the facade
            security.BindPdf(inputPath);

            // Decrypt the document using the owner (or user) password
            bool decrypted = security.DecryptFile(ownerPassword);
            if (!decrypted)
            {
                Console.Error.WriteLine("Decryption failed. Check the password.");
                return;
            }

            // Save the now‑decrypted PDF to a new file
            security.Save(outputPath);
            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}