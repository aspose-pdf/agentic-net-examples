using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";          // PDF that has been filled
        const string tempPath = "temp_filled.pdf";      // intermediate file
        const string outputPath = "protected.pdf";      // final protected PDF

        const string userPassword = "user123";          // user password
        const string ownerPassword = "owner123";        // owner password

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the filled PDF and save it (ensures a clean copy before encryption)
        using (Document doc = new Document(inputPath))
        {
            doc.Save(tempPath); // Save as PDF (no SaveOptions needed for PDF)
        }

        // Apply password protection using the PdfFileSecurity facade
        PdfFileSecurity fileSecurity = new PdfFileSecurity(tempPath, outputPath);
        // Encrypt with 256‑bit AES and allow printing (DocumentPrivilege.Print)
        bool success = fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

        if (!success)
        {
            Console.Error.WriteLine("Failed to encrypt the PDF.");
        }
        else
        {
            Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
        }

        // Remove the temporary file
        try { File.Delete(tempPath); } catch { }
    }
}