using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
            TextStamp stamp = new TextStamp("Sample Text");

            // Enable automatic font size adjustment to fit the stamp rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;
            stamp.AutoAdjustFontSizePrecision = 0.1f; // optional precision

            // Define the stamp rectangle size (width and height in points)
            stamp.Width = 200;   // desired width
            stamp.Height = 50;   // desired height

            // Position the stamp (example: bottom‑right corner of each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;

            // Optional styling of the text
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 24; // initial size; will be reduced if needed
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

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