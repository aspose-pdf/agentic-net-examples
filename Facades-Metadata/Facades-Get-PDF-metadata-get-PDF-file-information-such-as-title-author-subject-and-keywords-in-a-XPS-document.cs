using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output XPS file path
        const string xpsPath = "output.xps";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Retrieve PDF metadata using the PdfFileInfo facade
            // -----------------------------------------------------------------
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                Console.WriteLine("PDF Metadata:");
                Console.WriteLine($"Title   : {pdfInfo.Title}");
                Console.WriteLine($"Author  : {pdfInfo.Author}");
                Console.WriteLine($"Subject : {pdfInfo.Subject}");
                Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            }

            // -----------------------------------------------------------------
            // 2. Convert the PDF to XPS format
            // -----------------------------------------------------------------
            using (Document pdfDocument = new Document(pdfPath))
            {
                // XpsSaveOptions provides conversion settings; default options are sufficient here
                XpsSaveOptions xpsOptions = new XpsSaveOptions();

                // Save the document as XPS
                pdfDocument.Save(xpsPath, xpsOptions);
            }

            Console.WriteLine($"PDF successfully converted to XPS at '{xpsPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}