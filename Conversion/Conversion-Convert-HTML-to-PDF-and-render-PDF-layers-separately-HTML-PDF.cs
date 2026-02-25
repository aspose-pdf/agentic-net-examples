using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfWithLayers
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert HTML to PDF
        // -----------------------------------------------------------------
        // HtmlLoadOptions is in the Aspose.Pdf namespace; no extra using needed.
        // Wrap the Document in a using block for deterministic disposal.
        using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
        {
            // Save the document as PDF. No SaveOptions are required for PDF output.
            htmlDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML converted to PDF: {pdfPath}");

        // -----------------------------------------------------------------
        // 2. Render each PDF page (layer) as a separate PDF file
        // -----------------------------------------------------------------
        // In the context of this example, each page is treated as an
        // independent "layer".  Pages are 1‑based in Aspose.Pdf.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Converted PDF not found: {pdfPath}");
            return;
        }

        const string layersDir = "Layers";
        Directory.CreateDirectory(layersDir);

        using (Document pdfDoc = new Document(pdfPath))
        {
            for (int i = 1; i <= pdfDoc.Pages.Count; i++) // page-indexing-one-based rule
            {
                // Create a new document that will contain only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the page from the source document
                    singlePageDoc.Pages.Add(pdfDoc.Pages[i]);

                    // Save the single‑page PDF
                    string layerPath = Path.Combine(layersDir, $"layer_page_{i}.pdf");
                    singlePageDoc.Save(layerPath);
                    Console.WriteLine($"Saved layer {i} → {layerPath}");
                }
            }
        }

        Console.WriteLine("All layers have been rendered as separate PDF files.");
    }
}