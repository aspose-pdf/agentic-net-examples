using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Facades;      // For PdfPageStamp

class Program
{
    static void Main()
    {
        // Input PDF containing the graphical element (e.g., a page with a diagram)
        const string inputPath = "source.pdf";

        // Output PDF where the graphical element will be inserted onto a different page
        const string outputPath = "target.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Source page containing the graphical content to be copied (page 2)
            Page sourcePage = doc.Pages[2];

            // Destination page where the graphical content will be placed (page 1)
            Page targetPage = doc.Pages[1];

            // Create a stamp from the source page
            PdfPageStamp pageStamp = new PdfPageStamp(sourcePage);

            // Optionally adjust stamp properties (position, opacity, etc.)
            // For layout consistency we place the stamp at the same coordinates as the source page.
            // By default, Put() respects the source page size; additional transformations can be applied if needed.

            // Apply the stamp onto the target page
            pageStamp.Put(targetPage);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graphical element from page 2 has been inserted onto page 1 and saved to '{outputPath}'.");
    }
}