using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf types (Document, HtmlSaveOptions, HtmlLoadOptions)
using Aspose.Pdf.Annotations;   // For annotation import/export methods

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string htmlPath       = "output.html";
        const string finalPdfPath   = "final.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the original PDF and export its annotations to XFDF
            // -------------------------------------------------
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export all annotations to an XFDF file
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

                // -------------------------------------------------
                // 2. Convert the PDF to HTML (requires GDI+ on Windows)
                // -------------------------------------------------
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into the single HTML file for simplicity
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: generate only the body content if desired
                    // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
                };

                try
                {
                    pdfDoc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"PDF saved as HTML: {htmlPath}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion uses GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML export.");
                    return;
                }
            }

            // -------------------------------------------------
            // 3. Load the generated HTML back into a Document
            // -------------------------------------------------
            using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // -------------------------------------------------
                // 4. Import the previously exported annotations from XFDF
                // -------------------------------------------------
                htmlDoc.ImportAnnotationsFromXfdf(xfdfPath);
                Console.WriteLine("Annotations imported from XFDF into HTML-derived document.");

                // -------------------------------------------------
                // 5. Save the resulting document back to PDF
                // -------------------------------------------------
                htmlDoc.Save(finalPdfPath);
                Console.WriteLine($"Final PDF with imported annotations saved: {finalPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}