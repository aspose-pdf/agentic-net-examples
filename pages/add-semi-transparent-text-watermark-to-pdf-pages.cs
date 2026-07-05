using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a text stamp with the desired watermark text
                TextStamp stamp = new TextStamp("CONFIDENTIAL");

                // Set visual appearance
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 72;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // fill color
                stamp.Opacity = 0.3f;               // semi‑transparent fill
                stamp.OutlineOpacity = 0.8f;        // outline opacity
                stamp.OutlineWidth = 1.0f;          // outline stroke width

                // Position the stamp at the center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;
                stamp.Background = false; // draw on top of page content

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}