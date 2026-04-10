using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // ------------------------------------------------------------
        // 1. Determine the baseline Y coordinate of the first paragraph
        //    on page 5.  Use TextFragmentAbsorber with Pure extraction mode.
        // ------------------------------------------------------------
        double baselineY;
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages[5].Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("No text fragments were found on page 5.");
                return;
            }

            // The first fragment is assumed to belong to the target paragraph.
            TextFragment firstFragment = absorber.TextFragments[0];
            // For Pure mode the lower‑left Y (LLY) corresponds to the baseline.
            baselineY = firstFragment.Rectangle.LLY;
        }

        // ------------------------------------------------------------
        // 2. Reposition the existing stamp on page 5 so that its Y
        //    coordinate matches the paragraph baseline.
        // ------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        int pageNumber = 5;      // target page (1‑based)
        int stampIndex = 1;      // index of the stamp on that page (1‑based)
        double newX = 100;       // desired X coordinate (adjust as needed)
        double newY = baselineY; // align Y with the paragraph baseline

        editor.MoveStamp(pageNumber, stampIndex, newX, newY);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}