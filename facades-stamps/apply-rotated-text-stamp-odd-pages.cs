using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_odd_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Configure the text stamp
        TextStamp stamp = new TextStamp("Rotated Text");
        stamp.TextState.FontSize = 12;
        stamp.TextState.FontStyle = FontStyles.Bold;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
        stamp.HorizontalAlignment = HorizontalAlignment.Left;   // left margin
        stamp.VerticalAlignment   = VerticalAlignment.Top;    // top of the page
        stamp.XIndent = 0;   // distance from left edge
        stamp.YIndent = 0;   // distance from top edge
        stamp.RotateAngle = 30f; // rotate 30 degrees clockwise around the stamp centre

        // Apply the stamp to every odd‑numbered page
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
        {
            Page page = doc.Pages[pageNumber];
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
