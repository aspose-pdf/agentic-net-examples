using System;
using System.IO;
using System.Collections.Generic; // for Dictionary used by PdfPageEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for HtmlSaveOptions enum

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Facade: PdfPageEditor ----------
            // Edit page layout (rotation, zoom, page size) via the facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the facade (required before any operation)
                editor.BindPdf(doc);

                // Rotate the first page 90 degrees
                editor.PageRotations = new Dictionary<int, int> { { 1, 90 } };

                // Set a global zoom factor (80% of original size)
                editor.Zoom = 0.8f;

                // Change the output page size to A4
                editor.PageSize = PageSize.A4;

                // Apply all changes to the underlying Document
                editor.ApplyChanges();
            }

            // ---------- Generate HTML with full markup ----------
            // HtmlSaveOptions must be supplied explicitly (save-to-non-pdf rule)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Expose the complete HTML structure (underlying implementation details)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,

                // Embed all resources (images, CSS) into the HTML for a self‑contained file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Optional: prefix CSS class names to make the generated CSS identifiable
                CssClassNamesPrefix = "aspose_"
            };

            // Save the modified document as HTML
            doc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML generated at '{outputHtml}'.");
    }
}
