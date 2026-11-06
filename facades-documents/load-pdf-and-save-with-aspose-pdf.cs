using System;
using System.IO;
using Aspose.Pdf;
// If you need additional facade operations, you can also import:
// using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";

        // Verify the file exists before attempting to load it.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Load the PDF with the Document class (PdfFileEditor does not provide loading methods).
        Document pdfDoc = new Document(sourcePath);

        // Get the page count via the Document API.
        int pageCount = pdfDoc.Pages.Count;
        Console.WriteLine($"Loaded PDF '{sourcePath}' with {pageCount} pages.");

        // Additional operations can be performed with PdfFileEditor if needed.
        // Example (uncomment to use):
        // var editor = new Aspose.Pdf.Facades.PdfFileEditor();
        // editor.Split(sourcePath, "outputPages");
    }
}
