using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decrypted.pdf";
        const string password   = "ownerpass"; // owner or user password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Check whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (fileInfo.IsEncrypted)
        {
            // Decrypt only if the document is encrypted
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                // Bind the source PDF to the facade
                security.BindPdf(inputPath);

                // Try to decrypt; returns false instead of throwing an exception
                bool decrypted = security.TryDecryptFile(password);
                if (decrypted)
                {
                    // Save the decrypted PDF to the output path
                    security.Save(outputPath);
                    Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Decryption failed – incorrect password or unsupported encryption.");
                }
            }
        }
        else
        {
            // If not encrypted, simply copy the file (or you could load and save via Document)
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine($"File was not encrypted; copied to '{outputPath}'.");
        }
    }
}