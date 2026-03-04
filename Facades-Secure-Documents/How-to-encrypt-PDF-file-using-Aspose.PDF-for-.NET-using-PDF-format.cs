using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the encrypted output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSecurity facade with input and output file names
        // This constructor does not require a using block because the class is not IDisposable
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Encrypt the PDF:
        //   - userPassword: password required to open the document for reading
        //   - ownerPassword: password required to change permissions or remove encryption
        //   - privilege: allow only printing (other operations will be restricted)
        //   - keySize: 256‑bit encryption (AES is used automatically for this key size)
        bool encrypted = fileSecurity.TryEncryptFile(
            userPassword: "user123",
            ownerPassword: "owner123",
            privilege: DocumentPrivilege.Print,
            keySize: KeySize.x256);

        if (encrypted)
        {
            Console.WriteLine($"PDF encrypted successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to encrypt the PDF file.");
        }
    }
}