using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Example usage: retrieve the number of pages in the loaded document
            int pageCount = pdfDoc.Pages.Count;
            Console.WriteLine($"Loaded PDF has {pageCount} pages.");
        }
    }
}
