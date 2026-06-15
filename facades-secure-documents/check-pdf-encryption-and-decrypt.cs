using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine whether the PDF is encrypted.
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;

        if (!isEncrypted)
        {
            // No encryption – simply copy the file to the output location.
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("PDF is not encrypted; copied to output.");
            return;
        }

        // PDF is encrypted – attempt decryption using PdfFileSecurity.
        // Replace with the actual owner password if known; empty string works for user‑password only PDFs.
        const string ownerPassword = "ownerpass";

        // PdfFileSecurity constructor takes input and output file paths.
        using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
        {
            bool decrypted = security.TryDecryptFile(ownerPassword);
            if (decrypted)
            {
                Console.WriteLine("Decryption succeeded; output saved.");
            }
            else
            {
                Console.WriteLine("Decryption failed; verify the password.");
            }
        }
    }
}