using System;
using System.IO;
using System.Drawing; // for Rectangle and Color (Windows‑only GDI+)
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "underlined_output.pdf";
        const string phrase     = "specific phrase"; // phrase to underline

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document does not contain a fourth page.");
                return;
            }

            // Search for the phrase on page 4
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            // Restrict search to page 4 only
            doc.Pages[4].Accept(absorber);

            // Verify that the phrase was found
            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
                return;
            }

            // Get the first occurrence
            TextFragment fragment = absorber.TextFragments[0];

            // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle (required by PdfContentEditor)
            Aspose.Pdf.Rectangle pdfRect = fragment.Rectangle;
            int x      = (int)pdfRect.LLX;
            int y      = (int)pdfRect.LLY;
            int width  = (int)(pdfRect.URX - pdfRect.LLX);
            int height = (int)(pdfRect.URY - pdfRect.LLY);
            // Fully‑qualified System.Drawing.Rectangle to avoid CS0104 ambiguity
            System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(x, y, width, height);

            // Create markup (underline) using the Facades API
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);
                // type = 1 => underline, page = 4 (1‑based), color = Red (System.Drawing.Color)
                editor.CreateMarkup(drawRect, string.Empty, 1, 4, System.Drawing.Color.Red);
                editor.Save(outputPath);
            }

            Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
        }
    }
}
