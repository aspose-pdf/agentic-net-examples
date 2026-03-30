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

        using (Document doc = new Document(inputPath))
        {
            // Shadow stamp (slightly offset, gray color)
            TextStamp shadowStamp = new TextStamp(stampText);
            shadowStamp.HorizontalAlignment = HorizontalAlignment.Center;
            shadowStamp.VerticalAlignment = VerticalAlignment.Top;
            shadowStamp.TopMargin = 10f;
            shadowStamp.XIndent = 2f;
            shadowStamp.YIndent = -2f;
            shadowStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            shadowStamp.TextState.FontSize = 12;
            shadowStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            doc.Pages[1].AddStamp(shadowStamp);

            // Main stamp (black color)
            TextStamp mainStamp = new TextStamp(stampText);
            mainStamp.HorizontalAlignment = HorizontalAlignment.Center;
            mainStamp.VerticalAlignment = VerticalAlignment.Top;
            mainStamp.TopMargin = 10f;
            mainStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            mainStamp.TextState.FontSize = 12;
            mainStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            doc.Pages[1].AddStamp(mainStamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
    }
}