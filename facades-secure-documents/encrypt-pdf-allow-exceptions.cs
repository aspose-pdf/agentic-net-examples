using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade
            PdfFileSecurity security = new PdfFileSecurity();

            // Enable collection of exceptions instead of throwing them
            security.AllowExceptions = true;

            // Bind the source PDF file
            security.BindPdf(inputPdf);

            // Attempt encryption – returns true on success, false otherwise
            bool encrypted = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,   // example privilege
                KeySize.x256);             // 256‑bit encryption

            if (!encrypted)
            {
                // Encryption failed – retrieve detailed exception information
                Exception lastEx = security.LastException;
                Console.Error.WriteLine("Encryption failed.");
                if (lastEx != null)
                {
                    Console.Error.WriteLine($"Message: {lastEx.Message}");
                    if (lastEx.InnerException != null)
                    {
                        Console.Error.WriteLine($"Inner Exception: {lastEx.InnerException.Message}");
                    }
                }
                return;
            }

            // Save the encrypted PDF to the desired output path
            security.Save(outputPdf);
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions that were not captured by AllowExceptions
            Console.Error.WriteLine("An unexpected error occurred:");
            Console.Error.WriteLine($"Message: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}