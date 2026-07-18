using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For any facade usage if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF, intermediate HTML, and final DOCX paths
        const string inputPdfPath   = "input.pdf";
        const string intermediateHtmlPath = "intermediate.html";
        const string outputDocxPath = "output.docx";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Convert PDF to HTML and embed all fonts in the HTML
        // ------------------------------------------------------------
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HtmlSaveOptions
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Embed fonts in all three web formats (EOT, TTF, WOFF) to ensure they are available
            htmlOptions.FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats;

            // Embed all referenced resources (HTML, CSS, images, fonts) into a single HTML file
            htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the PDF as HTML
            pdfDocument.Save(intermediateHtmlPath, htmlOptions);
        }

        // ------------------------------------------------------------
        // Step 2: Load the generated HTML and convert it to DOCX
        // ------------------------------------------------------------
        using (Document htmlDocument = new Document(intermediateHtmlPath, new HtmlLoadOptions()))
        {
            // Initialize DocSaveOptions for DOCX output
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Output format DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use Flow mode for better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Ensure that fonts are re‑saved (embedded) in the resulting DOCX
                ReSaveFonts = true,

                // Convert Type3 fonts to TrueType where possible
                ConvertType3Fonts = true
            };

            // Save the document as DOCX with the specified options
            htmlDocument.Save(outputDocxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed. DOCX saved to '{outputDocxPath}'.");
    }
}