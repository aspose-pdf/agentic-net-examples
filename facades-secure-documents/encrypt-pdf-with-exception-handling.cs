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

        // Initialize the facade with input and output files
        PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
        // Enable collection of detailed exceptions instead of throwing
        security.AllowExceptions = true;

        // Attempt encryption; returns false on failure when AllowExceptions is true
        bool encrypted = security.TryEncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (!encrypted)
        {
            // Retrieve the detailed exception information
            Exception ex = security.LastException;
            if (ex != null)
            {
                Console.Error.WriteLine("Encryption failed:");
                Console.Error.WriteLine($"Message: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
            else
            {
                Console.Error.WriteLine("Encryption failed: unknown error.");
            }
        }
        else
        {
            Console.WriteLine($"Encryption succeeded. Output saved to '{outputPath}'.");
        }
    }
}