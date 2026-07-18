using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string phrase = "specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on page 4 (1‑based indexing)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Accept the absorber on the specific page (page index is 1‑based in the Pages collection)
            doc.Pages[4].Accept(absorber);

            // Ensure the phrase was found
            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("Phrase not found on page 4.");
                return;
            }

            // Get the first occurrence's rectangle (zero‑based index)
            TextFragment fragment = absorber.TextFragments[0];
            Aspose.Pdf.Rectangle pdfRect = fragment.Rectangle;

            // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle required by CreateMarkup
            System.Drawing.Rectangle drawingRect = new System.Drawing.Rectangle(
                (int)pdfRect.LLX,
                (int)pdfRect.LLY,
                (int)(pdfRect.URX - pdfRect.LLX),
                (int)(pdfRect.URY - pdfRect.LLY));

            // Create underline markup using PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);
                // type = 1 for underline, page = 4, color = Red (System.Drawing.Color)
                editor.CreateMarkup(drawingRect, string.Empty, 1, 4, System.Drawing.Color.Red);
                editor.Save(outputPath);
            }

            Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
        }
    }
}
