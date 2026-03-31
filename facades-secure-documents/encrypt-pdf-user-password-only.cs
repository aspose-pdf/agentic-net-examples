using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = ""; // undefined, will be replaced by a random string

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);
            // Encrypt with a user password only, no owner password, allow printing, 256‑bit AES encryption
            bool success = fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
            if (success)
            {
                Console.WriteLine("PDF encrypted successfully to " + outputPath);
            }
            else
            {
                Console.Error.WriteLine("Encryption failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}