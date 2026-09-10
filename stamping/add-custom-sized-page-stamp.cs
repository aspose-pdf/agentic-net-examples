using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages (source and target)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Source page that will be used as the stamp content (first page)
            Page sourcePage = doc.Pages[1];

            // Create a PdfPageStamp from the source page
            PdfPageStamp stamp = new PdfPageStamp(sourcePage);

            // Set custom dimensions for the stamp (in points)
            stamp.Width  = 200; // Desired width
            stamp.Height = 150; // Desired height

            // Position the stamp on the target page (second page)
            // XIndent and YIndent are measured from the left and bottom edges respectively
            stamp.XIndent = 100; // Horizontal offset from the left edge
            stamp.YIndent = 200; // Vertical offset from the bottom edge

            // Optional: make the stamp appear behind the page content
            stamp.Background = false; // true = background, false = foreground

            // Apply the stamp to the target page (second page)
            Page targetPage = doc.Pages[2];
            targetPage.AddStamp(stamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}