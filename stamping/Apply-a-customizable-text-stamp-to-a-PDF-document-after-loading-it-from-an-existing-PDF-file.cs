using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a customizable text stamp
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Positioning – center both horizontally and vertically on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Appearance settings
                Opacity = 0.5,                     // semi‑transparent
                Background = false,                // draw on top of page content
                // The TextAlignment enum may not exist in older Aspose.Pdf versions.
                // If your library version supports it, uncomment the line below.
                // TextAlignment = Aspose.Pdf.Text.TextAlignment.Center,
                // Adjust font size automatically to fit the page if needed
                AutoAdjustFontSizeToFitStampRectangle = true
            };

            // Configure the text state (font, size, color)
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}
