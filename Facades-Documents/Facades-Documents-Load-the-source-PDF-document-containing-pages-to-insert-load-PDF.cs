using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output copy
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "copy.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: Source PDF not found at '{sourcePdfPath}'.");
            return;
        }

        // Load the PDF using the Facade API
        // PdfFileInfo provides BindPdf methods for loading a document
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(sourcePdfPath);

        // Retrieve and display the number of pages in the loaded document
        int pageCount = pdfInfo.NumberOfPages;
        Console.WriteLine($"The PDF contains {pageCount} page(s).");

        // Save the loaded PDF to a new file (demonstrates the Facade save operation)
        pdfInfo.Save(outputPdfPath);
        Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
    }
}