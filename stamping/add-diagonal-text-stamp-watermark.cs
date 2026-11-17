using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added to resolve FontRepository

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a text stamp that will be used on every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent
                Opacity = 0.3f,

                // Rotate 45 degrees to create a diagonal watermark
                RotateAngle = 45f
            };

            // Configure the visual appearance of the stamp
            stamp.TextState.FontSize = 72;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            stamp.TextState.Font = FontRepository.FindFont("Arial"); // FontRepository now resolved

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF while the document is still alive
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
