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
            // PdfFileSecurity works directly with file paths; it does not require a Document instance.
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Use a predefined privilege that allows all operations.
                DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

                // Encrypt with 256‑bit key size and AES algorithm.
                bool success = security.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256, Algorithm.AES);

                if (!success)
                {
                    Console.Error.WriteLine("Encryption failed.");
                    return;
                }
            }

            Console.WriteLine($"File encrypted successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}