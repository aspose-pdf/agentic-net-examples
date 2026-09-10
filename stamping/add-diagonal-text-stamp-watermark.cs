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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be used on every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Rotate 45 degrees (arbitrary angle)
                RotateAngle = 45,
                // Make the stamp semi‑transparent
                Opacity = 0.3,
                // Optional: place stamp above content (default)
                Background = false
            };

            // Configure visual appearance of the text
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 72;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            stamp.TextState.FontStyle = FontStyles.Bold;

            // Apply the stamp to each page using Page.AddStamp (per‑page method)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}