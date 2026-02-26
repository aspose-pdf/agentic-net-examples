using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string outputPdf = "highlighted_output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load CGM (input‑only format) into a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were created from the CGM file.");
                return;
            }

            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle for the highlight annotation (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Yellow, // Yellow highlight
                Contents = "Highlighted text"    // Optional popup text
            };

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with highlight annotation saved to '{outputPdf}'.");
    }
}