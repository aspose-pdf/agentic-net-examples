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
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired value
            TextStamp stamp = new TextStamp(stampText);

            // Place the stamp on top of page content (default) and make it semi‑transparent
            stamp.Background = false;               // draw on top
            stamp.Opacity    = 0.6;                 // 60 % opacity

            // Define a background color for the text (e.g., black box) and foreground color (e.g., white text)
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;
            stamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black;

            // Center the stamp horizontally and vertically on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Optionally let the stamp auto‑adjust its font size to fit the rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;

            // Apply the stamp to every page in the document
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