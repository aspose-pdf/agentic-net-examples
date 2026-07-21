using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                // Use a standard font; FontRepository is in Aspose.Pdf.Text
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                // Use a gray color for the text
                ForegroundColor = Aspose.Pdf.Color.Gray
            };

            // Create a TextStamp with the desired text and the defined TextState
            TextStamp watermark = new TextStamp("CONFIDENTIAL", textState)
            {
                // Semi‑transparent appearance
                Opacity = 0.3,
                // Place the stamp behind the page content
                Background = true,
                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the same watermark to every page in the document
            foreach (Page page in doc.Pages)
            {
                // Add the stamp to the current page
                page.AddStamp(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}