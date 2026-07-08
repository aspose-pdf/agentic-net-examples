using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // needed for HorizontalAlignment, VerticalAlignment, etc.

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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define text appearance (white text on a dark background)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.White,
                BackgroundColor = Aspose.Pdf.Color.Black
            };

            // Create a TextStamp with the defined TextState
            TextStamp stamp = new TextStamp("CONFIDENTIAL", textState)
            {
                // Render as graphic operators so the background box is drawn
                Draw = true,
                // Place the stamp behind the page content
                Background = true,
                // Semi‑transparent background (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.6f,
                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Size of the background box
                Width = 200,
                Height = 50,
                // TextAlignment enum is not available in older Aspose.Pdf versions.
                // If using a newer version, uncomment the line below.
                // TextAlignment = Aspose.Pdf.Text.TextAlignment.Center
            };

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
