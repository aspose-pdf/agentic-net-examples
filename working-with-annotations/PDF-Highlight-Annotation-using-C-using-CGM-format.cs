using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";
        const string outputPdf = "highlighted_output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load the CGM file into a PDF document (CGM is input‑only)
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Ensure at least one page was created
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were loaded from the CGM file.");
                return;
            }

            // Add a highlight annotation to the first page
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Yellow,   // visual highlight color
                Opacity  = 0.5,                       // semi‑transparent
                Contents = "Sample highlighted text"
            };

            page.Annotations.Add(highlight);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with highlight annotation saved to '{outputPdf}'.");
    }
}