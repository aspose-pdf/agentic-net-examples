using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);

        try
        {
            // Attempt encryption; this method throws on failure.
            fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
            fileSecurity.Save(outputPath);
            Console.WriteLine($"Encryption succeeded. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.GetType().FullName} - {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"Inner exception: {ex.InnerException.GetType().FullName} - {ex.InnerException.Message}");
            }
        }
    }
}