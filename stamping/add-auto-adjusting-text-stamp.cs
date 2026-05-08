using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For TextState if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a textual stamp with the desired value
            TextStamp stamp = new TextStamp("Confidential");

            // Enable automatic font size adjustment so the text fits the stamp rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;

            // Optionally define the rectangle size (width/height) the stamp should occupy
            // If not set, the stamp uses the page size; here we set a smaller area.
            stamp.Width  = 200;   // desired width in points
            stamp.Height = 50;    // desired height in points

            // Position the stamp on the page (coordinates are from bottom‑left)
            stamp.XIndent = 100;  // horizontal offset
            stamp.YIndent = 700;  // vertical offset

            // Additional visual settings (optional)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;
            stamp.Opacity = 0.7f;   // semi‑transparent

            // Apply the stamp to each page (or a specific page)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}