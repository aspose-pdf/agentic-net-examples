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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        var document = new Document(inputPath);

        // Create a text stamp that will act as a background watermark
        var stamp = new TextStamp("CONFIDENTIAL");
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 48;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
        stamp.Opacity = 0.3f;               // 30% opacity
        stamp.Background = true;            // place behind page content
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Center;

        // Apply the stamp to every page
        foreach (Page page in document.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        document.Save(outputPath);

        Console.WriteLine($"Background stamp applied with 30% opacity: {outputPath}");
    }
}
