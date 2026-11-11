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

        // PdfPageEditor provides GetPageRotation (page numbers are 1‑based)
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(pdfPath);

        int pageNumber = 4;
        int rotationAngle = editor.GetPageRotation(pageNumber);

        Console.WriteLine($"Page {pageNumber} rotation angle: {rotationAngle} degrees");
    }
}