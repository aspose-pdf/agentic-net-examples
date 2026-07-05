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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the multi‑line disclaimer text
            string disclaimer = "This is a disclaimer.\nPlease read carefully.\nContact us for more info.";

            // Configure text appearance and custom line spacing
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black,
                // Additional spacing added after each line (in points)
                LineSpacing = 6f
            };

            // Create a TextStamp with the disclaimer and the configured TextState
            TextStamp stamp = new TextStamp(disclaimer, textState)
            {
                // Position the stamp at the bottom center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 20,   // distance from the bottom edge
                // Optional: make the stamp appear behind page content
                Background          = false,
                Opacity             = 1.0f
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disclaimer stamp to '{outputPath}'.");
    }
}