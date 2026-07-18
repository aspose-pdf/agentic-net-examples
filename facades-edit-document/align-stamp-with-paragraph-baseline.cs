using System;
using System.IO;
using System.Drawing; // only for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "tempStamped.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Determine the baseline position of the first paragraph on page 5
        // ------------------------------------------------------------
        double baselineY = 0;
        double baselineX = 0;

        using (Document doc = new Document(inputPath))
        {
            // Ensure page 5 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            // Extract text fragments from page 5 using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages[5].Accept(absorber);

            // Use the first fragment as a reference (adjust as needed)
            if (absorber.TextFragments.Count > 0)
            {
                // TextFragment collection is zero‑based
                TextFragment fragment = absorber.TextFragments[0];
                // Baseline Y is the lower‑left Y of the fragment rectangle
                baselineY = fragment.Rectangle.LLY;
                // Baseline X (left side) is the lower‑left X of the fragment rectangle
                baselineX = fragment.Rectangle.LLX;
            }
            else
            {
                Console.Error.WriteLine("No text fragments found on page 5.");
                return;
            }
        }

        // ------------------------------------------------------------
        // 2. Add a stamp (a simple text stamp) to the document
        // ------------------------------------------------------------
        // Create a Facades Stamp and bind a FormattedText object to it
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "Aligned Stamp",
            System.Drawing.Color.Black, // fully‑qualified to avoid ambiguity
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            12);
        stamp.BindLogo(ft);
        // Position will be overridden later, but set an initial origin for completeness
        stamp.SetOrigin((float)baselineX, (float)baselineY);
        stamp.Opacity = 0.8f;          // semi‑transparent
        stamp.IsBackground = false;    // draw on top of content

        // Use PdfFileStamp to embed the stamp into the PDF
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(tempPath);
        fileStamp.Close();

        // ------------------------------------------------------------
        // 3. Reposition the stamp so its baseline matches the paragraph baseline
        // ------------------------------------------------------------
        // MoveStamp changes the position of an existing stamp.
        // Stamp indices are 1‑based; the first stamp added to a page has index 1.
        Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.MoveStamp(pageNumber: 5, stampIndex: 1, x: (float)baselineX, y: (float)baselineY);
        editor.Save(outputPath);
        editor.Close();

        // Optional: clean up the intermediate file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
