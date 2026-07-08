using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Check whether the PDF is encrypted
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            bool isEncrypted = fileInfo.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            if (isEncrypted)
            {
                // Decrypt the PDF (owner password is required)
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // DecryptFile writes the decrypted PDF to the output file
                    security.DecryptFile(ownerPassword);
                }
                Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
            }
            else
            {
                // Encrypt the PDF with user and owner passwords
                using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
                {
                    // EncryptFile writes the encrypted PDF to the output file
                    security.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
                }
                Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
            }
        }
    }
}