using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for file-level operations
using Aspose.Pdf;          // Enums: DocumentPrivilege, KeySize, Algorithm

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
            // Initialize the PdfFileSecurity facade and bind the source PDF
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);

                // Encrypt with user/owner passwords, allow printing, 256‑bit AES encryption
                bool encrypted = security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256,
                    Algorithm.AES);

                if (!encrypted)
                {
                    Console.Error.WriteLine("Encryption failed.");
                    return;
                }

                // Save the encrypted PDF to the specified output file
                security.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}