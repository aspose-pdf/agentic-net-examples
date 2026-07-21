using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextState if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply stamp only on even pages
                if (i % 2 == 0)
                {
                    // Create a page number stamp with default format "#"
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Position the stamp at the bottom‑center of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                    // Optional: set visual appearance
                    stamp.TextState.FontSize = 12;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    stamp.Opacity = 0.8f;   // semi‑transparent

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page numbers added. Output saved to '{outputPath}'.");
    }
}