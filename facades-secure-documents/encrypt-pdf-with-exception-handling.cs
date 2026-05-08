using System;
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

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileSecurity facade
        PdfFileSecurity security = new PdfFileSecurity();

        // Enable exception capturing instead of throwing
        security.AllowExceptions = true;

        // Bind the source PDF file
        security.BindPdf(inputPath);

        // Attempt encryption (AES‑256 with Print privilege)
        bool success = security.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256,
            Algorithm.AES);

        // Save the result if encryption succeeded
        if (success)
        {
            security.Save(outputPath);
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Encryption failed.");
        }

        // Log detailed exception information if any
        if (security.LastException != null)
        {
            Console.Error.WriteLine("Exception details:");
            Console.Error.WriteLine($"Message: {security.LastException.Message}");
            if (security.LastException.InnerException != null)
            {
                Console.Error.WriteLine($"Inner Exception: {security.LastException.InnerException.Message}");
            }
        }

        // Clean up
        security.Close();
    }
}