using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (no password supplied)
        using (Document document = new Document(inputPath))
        {
            // Use PdfFileInfo to quickly check encryption status
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
            if (fileInfo.IsEncrypted)
            {
                CryptoAlgorithm? algorithm = document.CryptoAlgorithm;
                if (algorithm.HasValue)
                {
                    Console.WriteLine($"Encryption algorithm: {algorithm.Value}");
                }
                else
                {
                    Console.WriteLine("Encryption algorithm could not be determined.");
                }
            }
            else
            {
                Console.WriteLine("Document is not encrypted.");
            }
        }
    }
}