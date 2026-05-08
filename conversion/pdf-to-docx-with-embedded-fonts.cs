using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (includes HtmlLoadOptions, HtmlSaveOptions, DocSaveOptions)

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // Source PDF
        const string htmlPath = "output.html"; // Intermediate HTML
        const string docxPath = "output.docx"; // Final DOCX

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF → HTML with embedded fonts
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed fonts as WOFF files inside the HTML (ensures fonts are available)
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,
                // Embed all resources (images, CSS, fonts) directly into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            // Perform the conversion
            pdfDoc.Save(htmlPath, htmlOpts);
        }

        // -------------------------------------------------
        // Step 2: Load the generated HTML and save as DOCX
        // -------------------------------------------------
        // HtmlLoadOptions is required when loading HTML into a Document
        HtmlLoadOptions loadOpts = new HtmlLoadOptions();

        using (Document htmlDoc = new Document(htmlPath, loadOpts))
        {
            // Initialize DOCX save options
            DocSaveOptions docOpts = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Ensure that fonts are embedded into the resulting DOCX
                ReSaveFonts = true,
                // Convert Type3 fonts to TrueType where possible (helps embedding)
                ConvertType3Fonts = true
            };

            // Save the document as DOCX with embedded fonts
            htmlDoc.Save(docxPath, docOpts);
        }

        Console.WriteLine("PDF → HTML → DOCX conversion completed successfully.");
    }
}
