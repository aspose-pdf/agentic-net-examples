using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF using PdfFileSecurity facade.
        // The constructor binds the input and output files.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
        // Encrypt with user/owner passwords, allow printing, using 256‑bit AES.
        bool success = fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
        if (!success)
        {
            Console.Error.WriteLine("Encryption failed.");
        }
        else
        {
            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
    }
}