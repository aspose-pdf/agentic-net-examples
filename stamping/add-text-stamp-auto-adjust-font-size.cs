using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a textual stamp with the desired value
            TextStamp stamp = new TextStamp("Auto‑adjusted text stamp")
            {
                // Enable automatic font size adjustment to fit the stamp rectangle
                AutoAdjustFontSizeToFitStampRectangle = true,

                // Define the rectangle size (width & height) for the stamp
                Width = 200,   // desired width in points
                Height = 50,   // desired height in points

                // Position the stamp on the page
                XIndent = 100, // distance from left edge
                YIndent = 700  // distance from bottom edge
            };

            // Set text appearance via the existing TextState instance (TextState is read‑only)
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48; // initial size; will be reduced if needed
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the stamp to the first page
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
