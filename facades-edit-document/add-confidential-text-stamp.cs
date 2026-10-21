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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a text stamp with red font and a semi‑transparent red background
        TextStamp stamp = new TextStamp("Confidential")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            Background          = true,   // enable background rectangle
            Opacity             = 0.5     // 50 % transparency for the whole stamp
        };

        // Configure text appearance via the TextState instance
        stamp.TextState.Font           = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize       = 72;
        stamp.TextState.ForegroundColor = Color.Red;                     // red font
        stamp.TextState.BackgroundColor = Color.Red;                     // red background (blended with Opacity)

        // Load the PDF and apply the stamp to every page
        Document pdf = new Document(inputPath);
        foreach (Page page in pdf.Pages)
        {
            page.AddStamp(stamp);
        }
        pdf.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
