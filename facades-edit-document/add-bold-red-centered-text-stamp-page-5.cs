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

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Create a TextStamp with bold, red, centered text
        TextStamp stamp = new TextStamp("Sample Text")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center
        };
        // TextState is read‑only – modify the existing instance instead of assigning a new one
        stamp.TextState.Font            = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize        = 14;
        stamp.TextState.FontStyle       = FontStyles.Bold;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

        // Add the stamp to page 5 (pages are 1‑based)
        pdf.Pages[5].AddStamp(stamp);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Text stamp added to page 5 and saved as '{outputPath}'.");
    }
}
