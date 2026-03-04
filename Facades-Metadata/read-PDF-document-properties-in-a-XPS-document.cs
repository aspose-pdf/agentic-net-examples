using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xpsPath = "input.xps";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"File not found: {xpsPath}");
            return;
        }

        // Load the XPS file into a PDF Document using XpsLoadOptions
        XpsLoadOptions loadOptions = new XpsLoadOptions();
        using (Document pdfDoc = new Document(xpsPath, loadOptions))
        {
            // Bind the PdfFileInfo facade to the loaded Document to read metadata
            using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
            {
                Console.WriteLine($"Title: {info.Title}");
                Console.WriteLine($"Author: {info.Author}");
                Console.WriteLine($"Subject: {info.Subject}");
                Console.WriteLine($"Keywords: {info.Keywords}");
                Console.WriteLine($"Creator: {info.Creator}");
                Console.WriteLine($"Producer: {info.Producer}");
                Console.WriteLine($"CreationDate: {info.CreationDate}");
                Console.WriteLine($"ModDate: {info.ModDate}");
                Console.WriteLine($"Number of pages: {info.NumberOfPages}");
                Console.WriteLine($"PDF version: {info.GetPdfVersion()}");
                Console.WriteLine($"Is encrypted: {info.IsEncrypted}");
            }
        }
    }
}