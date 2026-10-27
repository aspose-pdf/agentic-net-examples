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
        const string watermark = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a text stamp that will be placed behind existing content
        TextStamp stamp = new TextStamp(watermark)
        {
            Background = true,                         // render behind page content
            Opacity = 0.3,                             // semi‑transparent
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        // TextState is read‑only – modify the existing instance instead of assigning a new one
        stamp.TextState.FontSize = 72;
        stamp.TextState.FontStyle = FontStyles.Bold;
        stamp.TextState.Font = FontRepository.FindFont("Arial");
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Apply the stamp to pages 2 through 5 (inclusive)
        for (int pageNumber = 2; pageNumber <= 5 && pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            pdfDocument.Pages[pageNumber].AddStamp(stamp);
        }

        // Save the result
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Watermark applied to pages 2‑5 and saved as '{outputPath}'.");
    }
}
