using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptExample
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Normal text fragment (baseline)
            TextFragment normal = new TextFragment("E=mc");
            normal.Position = new Position(100, 500);               // X,Y coordinates
            normal.TextState.FontSize = 12;                         // base font size
            page.Paragraphs.Add(normal);

            // Superscript fragment – raise its Y coordinate instead of using Rise (which does not exist)
            TextFragment superscript = new TextFragment("2");
            superscript.Position = new Position(150, 505);          // raise Y to appear as superscript
            superscript.TextState.FontSize = 8;                     // smaller than base
            page.Paragraphs.Add(superscript);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Superscript rendered and saved to '{outputPath}'.");
    }
}