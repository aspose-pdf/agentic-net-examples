using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a new PageNumberStamp for the current page
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20; // distance from the bottom edge (points)

                // Define visual appearance
                stamp.TextState.FontSize        = 12;
                stamp.TextState.Font            = FontRepository.FindFont("Helvetica");
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}