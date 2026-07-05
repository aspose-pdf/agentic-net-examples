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
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the security facade and bind the source PDF
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Bind the PDF to be processed
            security.BindPdf(inputPath);

            // Enable exception capturing instead of throwing
            security.AllowExceptions = true;

            // Attempt encryption with 256‑bit AES and Print privilege
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (!encrypted)
            {
                // Retrieve detailed error information from the facade
                Exception ex = security.LastException;
                Console.Error.WriteLine("Encryption failed.");
                if (ex != null)
                {
                    Console.Error.WriteLine($"Message: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            else
            {
                // Save the encrypted PDF to the output path
                security.Save(outputPath);
                Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
            }
        }
    }
}