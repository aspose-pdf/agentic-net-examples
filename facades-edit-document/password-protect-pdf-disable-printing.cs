using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF using PdfFileSecurity facade.
        // DocumentPrivilege.ForbidAll disables all permissions, including printing.
        // KeySize.x256 provides strong 256‑bit encryption.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            security.EncryptFile(
                userPassword:   "user123",
                ownerPassword:  "admin456",
                privilege:      DocumentPrivilege.ForbidAll,
                keySize:        KeySize.x256);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}