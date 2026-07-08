using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Check whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            // Not encrypted – simply copy the file
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("File is not encrypted; copied to output.");
            return;
        }

        // Encrypted – attempt decryption using the owner password
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            bool decrypted = security.TryDecryptFile(ownerPassword);
            if (decrypted)
                Console.WriteLine("Decryption succeeded.");
            else
                Console.WriteLine("Decryption failed. Verify the password.");
        }
    }
}