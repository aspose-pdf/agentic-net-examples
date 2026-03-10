using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths
        const string mhtInputPath  = "input.mht";
        const string pdfOutputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(mhtInputPath))
        {
            Console.Error.WriteLine($"File not found: {mhtInputPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document pdfDoc = new Document(mhtInputPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                // Set the highlight color (use Aspose.Pdf.Color, not System.Drawing.Color)
                Color = Aspose.Pdf.Color.Yellow,

                // Optional: add a comment that appears in the annotation popup
                Contents = "Highlighted text"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document as PDF (MHT cannot be saved directly)
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Highlight annotation added and PDF saved to '{pdfOutputPath}'.");
    }
}