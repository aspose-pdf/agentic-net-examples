using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, TextState, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor does not support AddStamp)
        Document doc = new Document(inputPath);

        // ---------------------------------------------------------------------
        // Create the *shadow* stamp (gray, slightly offset)
        // ---------------------------------------------------------------------
        TextStamp shadowStamp = new TextStamp(stampText)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Top,
            XIndent = 2,               // offset a few points to the right
            YIndent = -2,              // offset a few points down
            TopMargin = 10             // distance from the top edge
        };
        shadowStamp.TextState.Font = FontRepository.FindFont("Arial");
        shadowStamp.TextState.FontSize = 12;
        shadowStamp.TextState.ForegroundColor = Color.Gray;

        // ---------------------------------------------------------------------
        // Create the main stamp (black, positioned exactly on top of the shadow)
        // ---------------------------------------------------------------------
        TextStamp mainStamp = new TextStamp(stampText)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Top,
            TopMargin = 10            // same top margin as the shadow stamp
        };
        mainStamp.TextState.Font = FontRepository.FindFont("Arial");
        mainStamp.TextState.FontSize = 12;
        mainStamp.TextState.ForegroundColor = Color.Black;

        // ---------------------------------------------------------------------
        // Apply both stamps to the first page using Page.AddStamp
        // ---------------------------------------------------------------------
        Page firstPage = doc.Pages[1];
        firstPage.AddStamp(shadowStamp); // draw shadow first
        firstPage.AddStamp(mainStamp);   // draw main text on top of shadow

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Text stamp with shadow applied and saved to '{outputPath}'.");
    }
}
