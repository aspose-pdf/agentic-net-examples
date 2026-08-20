using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using a Facade class (PdfFileInfo)
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

        // Retrieve the underlying Document object
        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = fileInfo.Document)
        {
            // Verify successful loading by checking the page count
            if (doc != null && doc.Pages.Count > 0)
            {
                Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
            }
            else
            {
                Console.WriteLine("PDF loaded but contains no pages.");
            }
        }

        // No explicit disposal needed for PdfFileInfo (it does not implement IDisposable)
    }
}