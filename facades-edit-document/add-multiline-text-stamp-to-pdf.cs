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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Multiline text for the stamp
        string multilineText = "First line\nSecond line\nThird line";

        // Create a TextStamp and configure its appearance
        TextStamp textStamp = new TextStamp(multilineText);
        // Position settings
        textStamp.XIndent = 100;
        textStamp.YIndent = 500;
        textStamp.HorizontalAlignment = HorizontalAlignment.Left;
        textStamp.VerticalAlignment = VerticalAlignment.Bottom;
        // Opacity (1.0 = fully opaque)
        textStamp.Opacity = 0.9f;
        // Font settings – modify the existing TextState object
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 14;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

        // Apply the stamp to every page (or adjust as needed)
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}
