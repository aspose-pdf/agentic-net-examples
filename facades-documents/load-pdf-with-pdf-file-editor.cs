using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be loaded
        const string pdfPath = "destination.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF using the Document class (PdfFileEditor does not support BindPdf)
        Document pdfDoc = new Document(pdfPath);

        // PdfFileEditor can be used for file‑level operations (merge, split, etc.) without binding
        PdfFileEditor editor = new PdfFileEditor();

        // Example: display number of pages to confirm the PDF is loaded
        Console.WriteLine($"PDF '{pdfPath}' successfully loaded. Page count: {pdfDoc.Pages.Count}");

        // If you need to perform file‑level operations, call the appropriate PdfFileEditor methods directly, e.g.:
        // editor.Split(pdfPath, "output_page_{0}.pdf");
    }
}