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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a text stamp (watermark)
        TextStamp textStamp = new TextStamp("CONFIDENTIAL")
        {
            // Place the stamp behind the page content
            Background = true,
            // 50% opacity for a translucent effect
            Opacity = 0.5f,
            // Center the stamp on the page (optional)
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Configure the TextState (read‑only property – modify its members directly)
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 48;
        textStamp.TextState.FontStyle = FontStyles.Bold;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the watermarked PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}