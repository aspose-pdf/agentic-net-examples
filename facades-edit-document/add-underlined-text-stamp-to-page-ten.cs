using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a TextStamp with the desired appearance
        TextStamp textStamp = new TextStamp("Your Text Here");
        // Alignment (centered horizontally and vertically on the page)
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment   = VerticalAlignment.Center;
        // Background fill (yellow) – use TextState.BackgroundColor for the fill color
        textStamp.Background = true;
        textStamp.TextState.BackgroundColor = Color.Yellow;
        // Text formatting: underline, font, size, style, color
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 14;
        textStamp.TextState.FontStyle = FontStyles.Bold;
        textStamp.TextState.ForegroundColor = Color.Black; // use ForegroundColor, not FontColor
        textStamp.TextState.Underline = true;

        // Apply the stamp to page 10 using the Document API (PdfContentEditor has no AddStamp method)
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The PDF does not contain a page 10.");
                return;
            }

            // Add the stamp to the 10th page (pages are 1‑based indexed)
            doc.Pages[10].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
