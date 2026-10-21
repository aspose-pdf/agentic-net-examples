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
        const string stampText = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level API (Document)
        Document pdfDocument = new Document(inputPath);

        // ---------------------------------------------------------------------
        // Create the shadow stamp (gray, slightly offset) – this mimics a shadow
        // ---------------------------------------------------------------------
        TextStamp shadowStamp = new TextStamp(stampText);
        shadowStamp.TextState.Font = FontRepository.FindFont("Arial");
        shadowStamp.TextState.FontSize = 12;
        shadowStamp.TextState.ForegroundColor = Color.Gray; // shadow colour
        // Position the shadow – same alignment as the main stamp but with an offset
        shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
        shadowStamp.VerticalAlignment   = VerticalAlignment.Top;
        shadowStamp.XIndent = 1;   // offset X (points) – positive moves right
        shadowStamp.YIndent = 9;   // offset Y (points) – because YIndent is measured from bottom
        // The YIndent value is calculated as (top margin) - offsetY.
        // Since we use Top alignment with a 10‑point top margin, the effective Y position
        // for the shadow becomes 10 pts (top margin) - 1 pt (offsetY) = 9 pts.

        // ---------------------------------------------------------------
        // Create the actual visible stamp (black, no offset, on top of shadow)
        // ---------------------------------------------------------------
        TextStamp mainStamp = new TextStamp(stampText);
        mainStamp.TextState.Font = FontRepository.FindFont("Arial");
        mainStamp.TextState.FontSize = 12;
        mainStamp.TextState.ForegroundColor = Color.Black;
        mainStamp.HorizontalAlignment = HorizontalAlignment.Center;
        mainStamp.VerticalAlignment   = VerticalAlignment.Top;
        mainStamp.YIndent = 10; // distance from the top edge in points
        // No XIndent needed because we centre horizontally.

        // Add both stamps to the first page – shadow first, then the main stamp.
        Page firstPage = pdfDocument.Pages[1];
        firstPage.AddStamp(shadowStamp);
        firstPage.AddStamp(mainStamp);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp with shadow added and saved to '{outputPath}'.");
    }
}
