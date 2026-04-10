using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Facade class PdfFileInfo to bind (load) the PDF document.
        // PdfFileInfo does not implement IDisposable, so we manage it manually.
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(inputPath);

        // The bound Document can be accessed via the Document property.
        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = fileInfo.Document)
        {
            // Simple verification: check that the document has at least one page.
            if (doc != null && doc.Pages.Count > 0)
            {
                Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
            }
            else
            {
                Console.WriteLine("PDF loaded, but it contains no pages.");
            }
        }

        // No explicit Save is required for a load‑only operation.
        Console.WriteLine("Load verification completed.");
    }
}