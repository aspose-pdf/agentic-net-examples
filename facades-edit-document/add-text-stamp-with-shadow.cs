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

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Get the first page (pages are 1‑based).
        Page firstPage = pdfDocument.Pages[1];

        // ---------- Shadow stamp (gray, slightly offset) ----------
        TextStamp shadowStamp = new TextStamp(stampText);
        shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        shadowStamp.TextState.FontSize = 12;
        shadowStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray; // shadow color
        shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
        shadowStamp.VerticalAlignment = VerticalAlignment.Top;
        // Offset the shadow a few points downwards/rightwards.
        shadowStamp.YIndent = 22; // 2 points lower than the main stamp
        // Add the shadow stamp to the first page.
        firstPage.AddStamp(shadowStamp);

        // ---------- Main stamp (black) ----------
        TextStamp mainStamp = new TextStamp(stampText);
        mainStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        mainStamp.TextState.FontSize = 12;
        mainStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        mainStamp.HorizontalAlignment = HorizontalAlignment.Center;
        mainStamp.VerticalAlignment = VerticalAlignment.Top;
        mainStamp.YIndent = 20; // distance from the top edge
        // Add the main stamp to the first page.
        firstPage.AddStamp(mainStamp);

        // Save the modified PDF.
        pdfDocument.Save(outputPath);
    }
}
