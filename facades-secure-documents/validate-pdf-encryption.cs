using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Check whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        if (fileInfo.IsEncrypted)
        {
            // Decrypt only when encrypted, using PdfFileSecurity (no exception on failure)
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);
            bool decrypted = security.TryDecryptFile(ownerPassword);
            if (decrypted)
            {
                security.Save(outputPath);
                Console.WriteLine("Decrypted PDF saved to '" + outputPath + "'.");
            }
            else
            {
                Console.Error.WriteLine("Failed to decrypt the PDF. Incorrect password or other issue.");
            }
        }
        else
        {
            // Not encrypted – simply copy the file using Document
            using (Document doc = new Document(inputPath))
            {
                doc.Save(outputPath);
            }
            Console.WriteLine("PDF was not encrypted. Copied to '" + outputPath + "'.");
        }
    }
}