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
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        // Load the XPS file as a PDF document using XpsLoadOptions
        using (Document pdfDoc = new Document(xpsPath, new XpsLoadOptions()))
        {
            // Use the PdfFileInfo facade to read metadata
            using (PdfFileInfo fileInfo = new PdfFileInfo(pdfDoc))
            {
                string title  = fileInfo.Title;
                string author = fileInfo.Author;

                Console.WriteLine($"Title : {title}");
                Console.WriteLine($"Author: {author}");
            }
        }
    }
}