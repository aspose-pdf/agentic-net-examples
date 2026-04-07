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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Rotate the stamp 45 degrees (diagonal watermark)
                RotateAngle = 45,

                // Make the stamp semi‑transparent
                Opacity = 0.3f
            };

            // TextState is read‑only – modify the existing instance instead of assigning a new one
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 72;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Apply the stamp to each page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the result as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
