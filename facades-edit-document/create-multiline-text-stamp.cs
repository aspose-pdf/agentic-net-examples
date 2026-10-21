using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Multiline text for the stamp
        string stampText = "First line\r\nSecond line\r\nThird line";

        // Create and configure the TextStamp
        TextStamp textStamp = new TextStamp(stampText)
        {
            // Optional positioning – centre the stamp on each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center
        };

        // Set font, size and colour via TextState (the correct API in recent Aspose.Pdf versions)
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 14;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

        // Load the PDF using the high‑level Document API
        Document pdfDocument = new Document(inputPath);

        // Apply the stamp to each page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}