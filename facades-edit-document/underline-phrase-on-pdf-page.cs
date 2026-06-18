using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_underline.pdf";
        const string phrase     = "specific phrase"; // phrase to underline

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on the fourth page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            // Ensure the search is case‑sensitive / whole word as needed
            absorber.TextSearchOptions = new TextSearchOptions(true);
            // Apply absorber to page 4 (1‑based indexing)
            doc.Pages[4].Accept(absorber);

            // Get the first occurrence (if any)
            TextFragment fragment = absorber.TextFragments[1];
            if (fragment == null)
            {
                Console.Error.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
                return;
            }

            // Convert the Aspose.Pdf.Rectangle to a System.Drawing.Rectangle
            Aspose.Pdf.Rectangle pdfRect = fragment.Rectangle;
            var underlineRect = new System.Drawing.Rectangle(
                (int)pdfRect.LLX,
                (int)pdfRect.LLY,
                (int)(pdfRect.URX - pdfRect.LLX),
                (int)(pdfRect.URY - pdfRect.LLY));

            // Create the underline markup using PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);
            // type = 1 (Underline), page = 4, color = Red
            editor.CreateMarkup(
                underlineRect,
                string.Empty,   // contents (optional)
                1,               // markup type: 0=Highlight, 1=Underline, 2=StrikeOut, 3=Squiggly
                4,               // page number (1‑based)
                System.Drawing.Color.Red);

            // Save the modified document
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Underline annotation added. Saved to '{outputPath}'.");
        }
    }
}