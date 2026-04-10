using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity facade encrypts the PDF.
        // Owner password is passed as null, so a random owner password will be generated.
        // DocumentPrivilege.Print allows printing; KeySize.x256 selects 256‑bit AES encryption.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            bool encrypted = security.EncryptFile(userPassword, null, DocumentPrivilege.Print, KeySize.x256);
            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}