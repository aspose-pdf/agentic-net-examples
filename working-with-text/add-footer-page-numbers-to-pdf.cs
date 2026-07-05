using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PageNumberStamp (inherits from TextStamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a PageNumberStamp with default format "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom center of the page
                stamp.BottomMargin = 20;                     // distance from bottom edge
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with page numbers added: {outputPath}");
    }
}