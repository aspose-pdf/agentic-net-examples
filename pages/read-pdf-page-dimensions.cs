using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Allow the PDF path to be supplied via a command‑line argument; fall back to "sample.pdf".
        string inputPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists before trying to open it – this prevents the unhandled FileNotFoundException.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{Path.GetFullPath(inputPath)}'.");
            return;
        }

        try
        {
            // Load the document inside a using block so the file handle is released automatically.
            using (Document document = new Document(inputPath))
            {
                int pageCount = document.Pages.Count;
                Console.WriteLine($"Document contains {pageCount} page{(pageCount == 1 ? "" : "s")}." );

                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = document.Pages[i];
                    // The Rect property gives the page’s bounding box in points (1/72 inch).
                    Aspose.Pdf.Rectangle rect = page.Rect;
                    double width = rect.URX - rect.LLX;   // Upper‑right X minus lower‑left X
                    double height = rect.URY - rect.LLY; // Upper‑right Y minus lower‑left Y
                    Console.WriteLine($"Page {i}: Width = {width} pt, Height = {height} pt");
                }
            }
        }
        catch (Exception ex)
        {
            // Any unexpected errors (e.g., corrupted PDF) are reported without crashing the program.
            Console.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
