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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Prepare the text stamp (rotated 30°) that will be applied to odd‑numbered pages
        TextStamp oddPageStamp = new TextStamp("Rotated Stamp");
        oddPageStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        oddPageStamp.TextState.FontSize = 24;
        oddPageStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // fully‑qualified Aspose color
        oddPageStamp.RotateAngle = 30f;                     // 30° rotation (correct property)
        oddPageStamp.HorizontalAlignment = HorizontalAlignment.Left;   // left margin
        oddPageStamp.VerticalAlignment = VerticalAlignment.Center;    // roughly middle of page height
        oddPageStamp.XIndent = 10f;                     // distance from left edge
        oddPageStamp.YIndent = 0f;                      // no vertical offset (adjust if needed)
        oddPageStamp.Background = false;               // foreground stamp (correct property)
        oddPageStamp.Opacity = 1.0f;

        // Apply the stamp to every odd‑numbered page (1‑based indexing)
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            if (i % 2 == 1) // odd page
            {
                doc.Pages[i].AddStamp(oddPageStamp);
            }
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Rotated stamp applied to odd pages. Output saved to '{outputPath}'.");
    }
}
