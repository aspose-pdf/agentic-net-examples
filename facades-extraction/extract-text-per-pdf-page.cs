using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the document to obtain the total page count (PdfExtractor no longer exposes PageCount)
            Document doc = new Document(inputPdfPath);
            int pageCount = doc.Pages.Count;

            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document to the extractor once (binding is cheap)
                extractor.BindPdf(inputPdfPath);

                // Iterate over each page (Aspose.Pdf uses 1‑based page indexing)
                for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                {
                    // Set the page range to a single page
                    extractor.StartPage = pageNumber;
                    extractor.EndPage   = pageNumber;

                    // Extract text for the current page (required before GetText)
                    extractor.ExtractText();

                    // Build the output file name: Page1.txt, Page2.txt, ...
                    string outputPath = Path.Combine(outputFolder, $"Page{pageNumber}.txt");

                    // Write the extracted text to the file – GetText expects a file path, not a page number
                    extractor.GetText(outputPath);
                }
            }

            Console.WriteLine($"Text extracted to folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
