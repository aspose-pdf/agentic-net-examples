using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF meta‑information to check encryption status
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            bool isEncrypted = fileInfo.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            if (!isEncrypted)
            {
                // PDF is not encrypted – encrypt it using PdfFileSecurity
                using (PdfFileSecurity security = new PdfFileSecurity())
                {
                    // Initialize the facade with the source PDF
                    security.BindPdf(inputPath);

                    // Encrypt with user and owner passwords, allow printing only
                    security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x128);

                    // Save the encrypted PDF
                    security.Save(outputPath);
                }
            }
            else
            {
                // PDF is already encrypted – attempt to decrypt it
                using (PdfFileSecurity security = new PdfFileSecurity())
                {
                    security.BindPdf(inputPath);

                    // Decrypt using the owner password (fallback to user password if needed)
                    bool success = security.DecryptFile(ownerPassword);
                    if (success)
                    {
                        security.Save(outputPath);
                    }
                    else
                    {
                        Console.Error.WriteLine("Decryption failed.");
                    }
                }
            }
        }
    }
}