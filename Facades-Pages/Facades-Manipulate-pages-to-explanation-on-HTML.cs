using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Required for HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // Source PDF
        const string tempPdf = "temp_modified.pdf";   // Intermediate PDF after page edits
        const string outputHtml = "output.html";      // Final HTML output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Manipulate pages using PdfPageEditor (rotate & zoom)
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF to the facade
            pageEditor.BindPdf(inputPdf);

            // Example: affect only the first page
            pageEditor.ProcessPages = new int[] { 1 };

            // Rotate 90 degrees clockwise
            pageEditor.Rotation = 90;

            // Zoom to 120% (1.2 factor)
            pageEditor.Zoom = 1.2f;

            // Apply the changes to the document
            pageEditor.ApplyChanges();

            // Save the modified PDF to a temporary file
            pageEditor.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // 2. Convert the modified PDF to HTML (explicit HtmlSaveOptions)
        // ------------------------------------------------------------
        using (Document doc = new Document(tempPdf))
        {
            // HtmlSaveOptions must be supplied; otherwise the file is saved as PDF
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                // Save as HTML using the explicit options
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML file created: {outputHtml}");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }

        // ------------------------------------------------------------
        // 3. Clean up the temporary PDF file
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPdf);
        }
        catch
        {
            // Ignored – cleanup failure is non‑critical
        }
    }
}