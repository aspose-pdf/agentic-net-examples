using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp, TextState

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

        // Load the PDF document using the modern Document API
        Document pdfDocument = new Document(inputPath);

        // ------------------------------------------------------------
        // Shadow effect – create a gray stamp slightly offset from the
        // main stamp and place it behind the main stamp.
        // ------------------------------------------------------------
        TextStamp shadowStamp = new TextStamp(stampText);
        shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        shadowStamp.TextState.FontSize = 12;
        shadowStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray; // shadow colour
        shadowStamp.VerticalAlignment = VerticalAlignment.Top;
        shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
        // Slightly lower than the main stamp to simulate a shadow (Y‑offset)
        shadowStamp.YIndent = 22; // distance from the top edge
        // No IsBackground property on TextStamp – the shadow is added first so it renders behind the main stamp.

        // ------------------------------------------------------------
        // Main stamp – black text, same position as the shadow but
        // without the offset.
        // ------------------------------------------------------------
        TextStamp mainStamp = new TextStamp(stampText);
        mainStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        mainStamp.TextState.FontSize = 12;
        mainStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        mainStamp.VerticalAlignment = VerticalAlignment.Top;
        mainStamp.HorizontalAlignment = HorizontalAlignment.Center;
        mainStamp.YIndent = 20; // distance from the top edge

        // Add both stamps only to the first page (pages are 1‑based)
        Page firstPage = pdfDocument.Pages[1];
        firstPage.AddStamp(shadowStamp); // shadow added first → rendered behind
        firstPage.AddStamp(mainStamp);

        // Persist the changes
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
