using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for FontRepository if custom fonts need to be referenced

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output files
        const string htmlPath = "output.html";
        const string docxPath = "output.docx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert PDF to HTML and embed fonts in the HTML output
        // -----------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Save all referenced fonts in all common web formats (WOFF, TTF, EOT)
                // This ensures that the HTML can render with the original fonts.
                FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats,

                // Optional: embed resources directly into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Optional: use fixed layout to preserve the original appearance
                FixedLayout = true
            };

            // Perform the conversion
            pdfDocument.Save(htmlPath, htmlOptions);
        }

        // -----------------------------------------------------------------
        // Step 2: Convert the same PDF to DOCX and ensure custom fonts are embedded
        // -----------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOCX conversion options
            DocSaveOptions docOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Re‑save fonts on each page – forces embedding of used fonts
                ReSaveFonts = true,

                // Convert Type3 fonts to TrueType when possible (improves text extraction)
                ConvertType3Fonts = true,

                // Optional: improve recognition for editable flow layout
                Mode = DocSaveOptions.RecognitionMode.Flow
            };

            // Perform the conversion
            pdfDocument.Save(docxPath, docOptions);
        }

        Console.WriteLine($"Conversion completed:\nHTML -> {htmlPath}\nDOCX -> {docxPath}");
    }
}