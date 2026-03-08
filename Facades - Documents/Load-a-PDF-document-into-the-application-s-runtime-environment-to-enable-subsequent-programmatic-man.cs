using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the high‑level Document class to obtain metadata such as page count.
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count;
        Console.WriteLine($"Loaded PDF has {pageCount} page(s).");

        // If you need to use a Facade (e.g., PdfViewer) for rendering or other low‑level operations,
        // you can still bind the same file. The Facade does not expose GetPageCount, so we rely on
        // the Document object for that information.
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPath);
            // Additional manipulation can be performed here using other Facade APIs.
        }

        Console.WriteLine("PDF successfully loaded into the application runtime.");
    }
}
