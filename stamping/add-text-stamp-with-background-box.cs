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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired label
            TextStamp stamp = new TextStamp("CONFIDENTIAL");

            // Place the stamp behind the page content (background box)
            stamp.Background = true;

            // Make the background semi‑transparent
            stamp.Opacity = 0.6;

            // Center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Let the stamp automatically adjust its font size to fit the page rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;

            // Configure text appearance
            stamp.TextState.Font           = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize       = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;   // text color
            stamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black;   // background box color

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}