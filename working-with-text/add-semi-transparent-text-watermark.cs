using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, FontStyles

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp for the watermark
            TextStamp stamp = new TextStamp(watermarkText);

            // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
            stamp.Opacity = 0.3;

            // Center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Modify the existing TextState (do not assign a new TextState object)
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // Red color

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}