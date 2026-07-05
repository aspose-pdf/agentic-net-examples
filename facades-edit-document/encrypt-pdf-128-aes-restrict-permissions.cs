using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Passwords for the encrypted PDF
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSecurity to encrypt the PDF with 128‑bit AES and restrict all privileges
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            // DocumentPrivilege.ForbidAll disables printing, editing, copying, etc.
            bool success = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.ForbidAll,
                KeySize.x128,
                Algorithm.AES);

            if (!success)
            {
                Console.Error.WriteLine("Failed to encrypt the PDF.");
            }
            else
            {
                Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
            }
        }
    }
}