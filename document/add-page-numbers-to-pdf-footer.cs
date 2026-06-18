using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a PageNumberStamp – default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp in the footer (centered at the bottom)
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin       = 20; // distance from the bottom edge

                // Optional: style the page number text
                stamp.TextState.FontSize = 12;
                stamp.TextState.Font      = FontRepository.FindFont("Helvetica");
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}