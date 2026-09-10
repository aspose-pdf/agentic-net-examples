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

        try
        {
            // PdfFileSecurity implements IDisposable, so use a using block.
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Enable exception capturing instead of throwing.
                security.AllowExceptions = true;

                // Bind the source PDF file.
                security.BindPdf(inputPath);

                // Attempt encryption. Returns true on success.
                bool encrypted = security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                if (!encrypted)
                {
                    // Encryption failed – retrieve detailed info from LastException.
                    Exception ex = security.LastException;
                    Console.Error.WriteLine("Encryption failed.");
                    if (ex != null)
                    {
                        Console.Error.WriteLine($"Message: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                        }
                    }
                }
                else
                {
                    // Save the encrypted PDF.
                    security.Save(outputPath);
                    Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (should be rare because AllowExceptions suppresses throws).
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}