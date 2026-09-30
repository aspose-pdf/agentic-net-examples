using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the last page to extract
        const string inputPath  = "input.pdf";
        const string outputPath = "extracted.pdf";
        const string endPageStr = "5"; // change as needed

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the end page number
        if (!int.TryParse(endPageStr, out int endPage) || endPage < 1)
        {
            Console.Error.WriteLine($"Invalid end page number: {endPageStr}");
            return;
        }

        try
        {
            // Load the source PDF using Document (PdfFileEditor does not expose ExtractPages in this version)
            Document sourceDoc = new Document(inputPath);

            // Ensure the requested end page does not exceed the source document page count
            if (endPage > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Requested end page ({endPage}) exceeds source page count ({sourceDoc.Pages.Count}).");
                return;
            }

            // Create a new document to hold the extracted pages
            Document extractedDoc = new Document();

            // Copy pages 1 through endPage from the source document
            for (int pageNumber = 1; pageNumber <= endPage; pageNumber++)
            {
                // Import the page into the new document (preserves resources)
                extractedDoc.Pages.Add(sourceDoc.Pages[pageNumber]);
            }

            // Save the extracted pages to the output file
            extractedDoc.Save(outputPath);

            Console.WriteLine($"Pages 1-{endPage} extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
