using System;
using System.IO;
using Aspose.Pdf;               // DocumentPrivilege, KeySize enums
using Aspose.Pdf.Facades;      // PdfFileSecurity facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "encrypted.pdf"; // encrypted PDF
        const string userPassword  = "user123";    // password required to open the file
        const string ownerPassword = "owner123";   // password that grants full rights

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with the source and destination file names
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt the PDF:
        // - userPassword / ownerPassword as defined above
        // - DocumentPrivilege.Print allows printing only (adjust as needed)
        // - KeySize.x256 selects strong 256‑bit AES encryption
        bool encrypted = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (encrypted)
            Console.WriteLine($"Encryption succeeded. Encrypted file saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Encryption failed.");
    }
}