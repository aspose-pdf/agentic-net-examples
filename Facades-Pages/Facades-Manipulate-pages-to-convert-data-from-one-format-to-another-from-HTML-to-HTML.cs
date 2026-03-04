using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (Document, HtmlLoadOptions, HtmlSaveOptions)
using Aspose.Pdf.Facades;        // Facade API (PdfPageEditor)

class Program
{
    static void Main()
    {
        // Input HTML file (source format)
        const string inputHtml  = "input.html";
        // Output HTML file (target format)
        const string outputHtml = "output.html";

        if (!File.Exists(inputHtml))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtml}");
            return;
        }

        // Load the HTML document into a Pdf Document object.
        // HtmlLoadOptions tells Aspose.Pdf that the source format is HTML.
        using (Document doc = new Document(inputHtml, new HtmlLoadOptions()))
        {
            // -----------------------------------------------------------------
            // Use a Facade (PdfPageEditor) to manipulate pages.
            // Example: rotate all pages 90 degrees clockwise.
            // -----------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);          // Bind the Document to the facade.
            pageEditor.Rotation = 90;         // Set desired rotation.
            pageEditor.ApplyChanges();        // Apply the changes to the document.

            // Save the modified document back to HTML.
            // HtmlSaveOptions is required for non‑PDF output (rule: save-to-non-pdf-always-use-save-options).
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Example option: embed all resources into the single HTML file.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            doc.Save(outputHtml, htmlOptions);
        }

        Console.WriteLine($"HTML conversion completed. Output saved to '{outputHtml}'.");
    }
}