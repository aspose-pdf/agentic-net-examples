using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for PageSize (if not already included)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Append an empty page at the end of the document
            Page addedPage = doc.Pages.Add();

            // Resize the newly added page to A4 dimensions
            // PageSize.A4 provides width and height in points
            addedPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with an extra A4 page: {outputPath}");
    }
}