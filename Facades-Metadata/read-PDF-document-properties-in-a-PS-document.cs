using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo provides access to PDF metadata via the Facades API.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Standard document properties
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Creator: {pdfInfo.Creator}");
            Console.WriteLine($"Producer: {pdfInfo.Producer}");
            Console.WriteLine($"Creation Date: {pdfInfo.CreationDate}");
            Console.WriteLine($"Modification Date: {pdfInfo.ModDate}");
            Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"PDF Version: {pdfInfo.GetPdfVersion()}");
            Console.WriteLine($"Is Encrypted: {pdfInfo.IsEncrypted}");
            Console.WriteLine($"Has Open Password: {pdfInfo.HasOpenPassword}");
            Console.WriteLine($"Has Edit Password: {pdfInfo.HasEditPassword}");

            // Example of retrieving a custom metadata entry
            string customValue = pdfInfo.GetMetaInfo("CustomKey");
            if (!string.IsNullOrEmpty(customValue))
            {
                Console.WriteLine($"CustomKey: {customValue}");
            }
        }
    }
}