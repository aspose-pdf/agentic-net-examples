using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputHtmlPath = "input.html";
        const string outputHtmlPath = "refreshed_output.html";

        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        // Load HTML into a PDF document.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        using (Document pdfDoc = new Document(inputHtmlPath, loadOptions))
        {
            // -----------------------------------------------------------------
            // Page manipulation using the PdfPageEditor facade.
            // -----------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(pdfDoc);

            // 1. Uniform zoom factor (110%).
            pageEditor.Zoom = 1.1f;

            // 2. Rotate all pages 90° clockwise.
            pageEditor.Rotation = 90;

            // 3. Center content horizontally and vertically.
            // Use the current (non‑obsolete) enum types.
            pageEditor.HorizontalAlignment = HorizontalAlignment.Center; // HorizontalAlignment is fine (maps to HorizontalAlignmentType)
            pageEditor.VerticalAlignment = VerticalAlignmentType.Center;   // Correct enum from Facades namespace

            // Apply the changes.
            pageEditor.ApplyChanges();

            // -----------------------------------------------------------------
            // Save the modified document back to HTML.
            // -----------------------------------------------------------------
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            pdfDoc.Save(outputHtmlPath, saveOptions);
        }

        Console.WriteLine($"Refreshed HTML saved to '{outputHtmlPath}'.");
    }
}
