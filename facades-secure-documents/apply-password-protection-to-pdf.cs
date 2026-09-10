using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege, KeySize enums
using Aspose.Pdf.Facades;      // PdfFileSecurity facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "protected.pdf";  // encrypted PDF
        const string userPassword  = "user123";    // password required to open
        const string ownerPassword = "owner123";   // password required to change permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity works directly on file paths; no Document object is created.
        // Wrap it in a using block because it implements IDisposable.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // Encrypt the PDF: allow only printing, use 256‑bit AES encryption.
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,   // allowed operation
                KeySize.x256);             // 256‑bit key size

            if (!encrypted)
            {
                Console.Error.WriteLine("Failed to encrypt the PDF.");
                return;
            }
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}