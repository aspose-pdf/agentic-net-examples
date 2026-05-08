using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Check whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (fileInfo.IsEncrypted)
        {
            // Decrypt the encrypted PDF using the owner password
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);
            bool decrypted = security.TryDecryptFile(ownerPassword);
            if (decrypted)
                Console.WriteLine($"Decryption succeeded. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Decryption failed. Check the password.");
        }
        else
        {
            // If not encrypted, simply copy the file to the output location
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine($"File is not encrypted. Copied to '{outputPath}'.");
        }
    }
}