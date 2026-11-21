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

        // Load the PDF with the high‑level Document API (PdfContentEditor does not support AddTextStamp)
        Document doc = new Document(inputPath);

        // Create a multi‑line text stamp
        TextStamp stamp = new TextStamp("First line of watermark\nSecond line of watermark\nThird line of watermark");

        // Configure text appearance – TextState is mutable, set its members directly
        stamp.TextState.Font = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Color.Black;
        // Set line spacing (leading) to 1.5 × font size for better readability
        stamp.TextState.LineSpacing = 1.5f * 36;

        // Position the stamp at the centre of each page and make it semi‑transparent
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Center;
        stamp.Opacity = 0.5;

        // Apply the stamp to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the result
        doc.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
