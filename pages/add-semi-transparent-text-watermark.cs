using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define text appearance
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.Red // fill color
            };

            // Create a text stamp with the desired opacity and outline settings
            TextStamp stamp = new TextStamp(watermarkText, textState)
            {
                Opacity = 0.3f,            // semi‑transparent fill
                OutlineOpacity = 0.8f,    // outline (stroke) opacity
                OutlineWidth = 1.5f,      // outline thickness
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = false        // draw on top of page content
            };

            // Apply the stamp to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}