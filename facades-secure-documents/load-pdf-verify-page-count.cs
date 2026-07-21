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

        // Use the PdfFileInfo facade to bind the PDF file
        // PdfFileInfo implements IDisposable, so wrap it in a using block
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the PDF document to the facade
            pdfInfo.BindPdf(inputPath);

            // Retrieve the underlying Document object from the facade
            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = pdfInfo.Document)
            {
                // Verify successful loading by checking that the document has at least one page
                if (doc != null && doc.Pages.Count > 0)
                {
                    Console.WriteLine($"PDF loaded successfully. Page count: {doc.Pages.Count}");
                }
                else
                {
                    Console.WriteLine("Failed to load PDF or document contains no pages.");
                }
            }
        }
    }
}