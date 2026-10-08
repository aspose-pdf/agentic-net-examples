using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF with the Facade class
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(pdfPath);

        // Aspose.Pdf uses 1‑based page indexing
        int pageNumber = 4;
        int rotationAngle = pageEditor.GetPageRotation(pageNumber);

        Console.WriteLine($"Page {pageNumber} rotation angle: {rotationAngle} degrees");
    }
}