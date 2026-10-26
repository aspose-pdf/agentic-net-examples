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

        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will act as the watermark
            TextStamp stamp = new TextStamp("Confidential")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                TopMargin           = 20,
                Opacity             = 0.5
            };

            // Configure the visual appearance via the existing TextState instance
            stamp.TextState.Font       = FontRepository.FindFont("Arial");
            stamp.TextState.FontSize   = 48;
            stamp.TextState.FontStyle  = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // pure red

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
