using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "protected.pdf";  // destination PDF
        const string userPassword  = "user123";    // password required to open the file
        const string ownerPassword = "owner123";   // password required to change permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileSecurity works directly on files, no Document object is created.
        // The constructor (inputFile, outputFile) initializes the facade.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt the PDF:
        //   - DocumentPrivilege.Print allows printing.
        //   - KeySize.x256 selects 256‑bit AES encryption.
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (encrypted)
            Console.WriteLine($"PDF encrypted successfully: {outputPath}");
        else
            Console.Error.WriteLine("Failed to encrypt the PDF.");
    }
}