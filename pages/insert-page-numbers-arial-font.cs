using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for FontRepository and TextState

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
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Configure the text appearance: Arial, 14 points
                stamp.TextState.Font = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize = 14;

                // Optional: set alignment and margins as desired
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin = 20; // distance from bottom edge

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}