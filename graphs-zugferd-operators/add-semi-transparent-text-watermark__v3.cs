using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class WatermarkExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a semi‑transparent text stamp
                TextStamp stamp = new TextStamp("CONFIDENTIAL");

                // Place the stamp behind existing page content
                stamp.Background = true;               // true = behind page content
                stamp.Opacity    = 0.3;                // 30 % opacity

                // Center the stamp on the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Define visual appearance of the text – modify the existing TextState instance
                stamp.TextState.Font        = FontRepository.FindFont("Arial");
                stamp.TextState.FontSize    = 72;
                stamp.TextState.FontStyle   = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.FromRgb(0.5, 0.5, 0.5); // light gray

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
