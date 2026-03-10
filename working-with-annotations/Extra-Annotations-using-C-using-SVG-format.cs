using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Vector;   // for SvgSaveOptions
using Aspose.Pdf.Text;     // for TextIcon if needed

class Program
{
    static void Main()
    {
        // Input PDF path – replace with your actual file.
        const string inputPdfPath = "input.pdf";

        // Output SVG path – the PDF will be saved as SVG.
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Ensure the document has at least one page.
                if (pdfDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The PDF has no pages.");
                    return;
                }

                // Work with the first page (1‑based indexing).
                Page page = pdfDoc.Pages[1];

                // ------------------------------------------------------------
                // Add a Text annotation (a sticky note) to the page.
                // ------------------------------------------------------------
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation textAnn = new TextAnnotation(page, textRect)
                {
                    Title    = "Extra Note",
                    Contents = "This is an extra text annotation added via code.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Icon     = TextIcon.Note,
                    Open     = true
                };
                page.Annotations.Add(textAnn);

                // ------------------------------------------------------------
                // Add a Link annotation that points to an external URL.
                // ------------------------------------------------------------
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 400, 250, 420);
                LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
                {
                    Color = Aspose.Pdf.Color.Blue
                };
                // Use GoToURIAction for external hyperlinks (Hyperlink property is not a string).
                linkAnn.Action = new GoToURIAction("https://www.example.com");
                page.Annotations.Add(linkAnn);

                // ------------------------------------------------------------
                // Save the modified document as SVG.
                // ------------------------------------------------------------
                // SvgSaveOptions lives in the Aspose.Pdf namespace (no Aspose.Pdf.Saving).
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    // Example option: treat each page as a separate file inside a directory.
                    // If false, a single file is created for a single‑page PDF.
                    TreatTargetFileNameAsDirectory = true
                };

                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine($"PDF with extra annotations saved as SVG to '{outputSvgPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}