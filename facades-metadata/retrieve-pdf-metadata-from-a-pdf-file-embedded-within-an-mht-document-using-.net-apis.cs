using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file as a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Document is loaded inside a using block for proper disposal
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Initialize the PdfFileInfo facade with the loaded document
            PdfFileInfo info = new PdfFileInfo(pdfDoc);

            // Retrieve and display PDF metadata
            Console.WriteLine($"Title: {info.Title}");
            Console.WriteLine($"Author: {info.Author}");
            Console.WriteLine($"Subject: {info.Subject}");
            Console.WriteLine($"Keywords: {info.Keywords}");
            Console.WriteLine($"Creator: {info.Creator}");
            Console.WriteLine($"Producer: {info.Producer}");
            Console.WriteLine($"CreationDate: {info.CreationDate}");
            Console.WriteLine($"ModDate: {info.ModDate}");
            Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
            Console.WriteLine($"Is Encrypted: {info.IsEncrypted}");
        }
    }
}