using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextStamp and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a TextStamp for the current page number with leading zeros (e.g., 01, 02 …)
                string pageNumber = i.ToString("D2"); // D2 pads single‑digit numbers with a leading zero
                TextStamp stamp = new TextStamp(pageNumber);

                // Position the stamp at the bottom‑center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20; // distance from the bottom edge

                // Optional: style the text (font, size, color)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 12;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF (using the provided save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with leading zeros saved to '{outputPath}'.");
    }
}