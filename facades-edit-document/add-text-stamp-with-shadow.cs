using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ----- Shadow stamp (gray, slightly offset) -----
        TextStamp shadowStamp = new TextStamp(stampText);
        shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        shadowStamp.TextState.FontSize = 12;
        shadowStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
        shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
        shadowStamp.VerticalAlignment   = VerticalAlignment.Top;
        shadowStamp.TopMargin = 10;               // same top margin as main stamp
        shadowStamp.XIndent = 1;                  // slight horizontal offset for shadow effect
        shadowStamp.YIndent = -1;                 // slight vertical offset (upwards) for shadow effect

        // ----- Main stamp (black) -----
        TextStamp textStamp = new TextStamp(stampText);
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 12;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment   = VerticalAlignment.Top;
        textStamp.TopMargin = 10;

        // Load the PDF document and add the stamps to the first page (shadow first, then main)
        Document pdfDocument = new Document(inputPath);
        pdfDocument.Pages[1].AddStamp(shadowStamp);
        pdfDocument.Pages[1].AddStamp(textStamp);
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp added to '{outputPath}'.");
    }
}
