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

        // Load the source PDF
        Document pdfDocument = new Document(inputPath);

        // Create a text stamp with the required appearance
        TextStamp textStamp = new TextStamp("Confidential");
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 36;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;               // red text
        textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(255,255,255); // optional white background
        textStamp.Opacity = 0.5f;                                                // 50 % opacity (semi‑transparent)
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;            // centre horizontally
        textStamp.VerticalAlignment   = VerticalAlignment.Center;              // centre vertically

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
