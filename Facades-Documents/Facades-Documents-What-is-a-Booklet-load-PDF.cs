using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be examined
        const string pdfPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF using a Facade class (PdfFileInfo) – this avoids direct use of Document
        // and demonstrates how to work with PDFs via the Aspose.Pdf.Facades namespace.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Output basic document information
            Console.WriteLine($"Title          : {pdfInfo.Title}");
            Console.WriteLine($"Author         : {pdfInfo.Author}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
        }

        // Explanation of what a "booklet" means in the context of PDFs.
        // A booklet is a PDF that has been arranged (imposed) so that when printed
        // on both sides of a sheet and folded, the pages appear in the correct order.
        // This typically involves reordering and scaling pages, which can be performed
        // with the PdfBooklet class (not shown here) or similar imposition tools.
        Console.WriteLine("\nA booklet is a PDF formatted for printing as a folded booklet,");
        Console.WriteLine("where pages are reordered and possibly scaled so that, after");
        Console.WriteLine("printing double‑sided and folding, the reading order is correct.");
    }
}