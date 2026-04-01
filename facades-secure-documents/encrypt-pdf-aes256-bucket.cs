using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist in the working directory)
        string inputPath = "input.pdf";
        // Name of the encrypted file to be stored in the bucket
        string outputFileName = "encrypted.pdf";
        // Simulated cloud storage bucket folder
        string bucketFolder = "bucket";

        // Ensure the bucket folder exists
        Directory.CreateDirectory(bucketFolder);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the security facade
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        // Bind the source PDF
        fileSecurity.BindPdf(inputPath);

        // Encrypt using AES‑256 (KeySize.x256 with Algorithm.AES)
        bool encryptionResult = fileSecurity.EncryptFile(
            "user123",
            "owner123",
            DocumentPrivilege.Print,
            KeySize.x256,
            Algorithm.AES);

        if (!encryptionResult)
        {
            Console.Error.WriteLine("Encryption failed.");
            return;
        }

        // Save the encrypted PDF into the bucket folder
        string destinationPath = Path.Combine(bucketFolder, outputFileName);
        fileSecurity.Save(destinationPath);

        Console.WriteLine($"Encrypted PDF saved to bucket: {destinationPath}");
    }
}