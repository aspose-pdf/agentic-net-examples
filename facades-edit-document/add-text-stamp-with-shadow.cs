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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (Document is disposed automatically)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page
            Page firstPage = doc.Pages[1];

            // ----- Shadow stamp (gray, slightly lower) -----
            TextStamp shadowStamp = new TextStamp(stampText);
            shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            shadowStamp.TextState.FontSize = 12;
            shadowStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray; // shadow color
            shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
            shadowStamp.VerticalAlignment = VerticalAlignment.Top;
            // Offset the shadow a few points down from the main stamp
            shadowStamp.TopMargin = 12; // 2 points lower than the main stamp

            // ----- Main stamp (black) -----
            TextStamp mainStamp = new TextStamp(stampText);
            mainStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            mainStamp.TextState.FontSize = 12;
            mainStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            mainStamp.HorizontalAlignment = HorizontalAlignment.Center;
            mainStamp.VerticalAlignment = VerticalAlignment.Top;
            mainStamp.TopMargin = 10; // distance from the top edge

            // Add both stamps to the first page (shadow first, then main)
            firstPage.AddStamp(shadowStamp);
            firstPage.AddStamp(mainStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp with shadow added. Output saved to '{outputPath}'.");
    }
}
