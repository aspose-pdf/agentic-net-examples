using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("Confidential");

            // Define the stamp rectangle size (example: 200x50 points)
            stamp.Width = 200;
            stamp.Height = 50;

            // Center the stamp on the page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Enable automatic font size adjustment to fit the rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;

            // Optional visual settings
            stamp.Background = false;                     // draw on top of content
            stamp.Opacity = 0.5f;                         // semi‑transparent
            stamp.TextState.FontSize = 48;                // initial size (may be reduced)
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

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