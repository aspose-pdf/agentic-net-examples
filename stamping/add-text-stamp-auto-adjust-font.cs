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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: using block ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a textual stamp with the desired content
            TextStamp stamp = new TextStamp("Confidential");

            // Define the stamp rectangle size (width & height in points)
            stamp.Width  = 200; // desired width
            stamp.Height = 50;  // desired height

            // Enable automatic font size adjustment to fit the rectangle
            stamp.AutoAdjustFontSizeToFitStampRectangle = true;

            // Optional: center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}