using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, SimpleFontSubstitution, HtmlLoadOptions, HtmlSaveOptions, DocSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string htmlPath       = "intermediate.html";
        const string outputDocxPath = "output.docx";
        const string customFontPath = "MyCustomFont.ttf"; // path to the font to embed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to HTML (Windows‑only, GDI+)
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                pdfDoc.Save(htmlPath, htmlOpts);
                Console.WriteLine($"PDF converted to HTML: {htmlPath}");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation aborted.");
                return;
            }
        }

        // -------------------------------------------------
        // Step 2: Load the generated HTML and embed fonts
        // -------------------------------------------------
        // Register the custom font so that it can be used during conversion.
        // The substitution maps a logical font name (any name you choose) to the actual TTF file.
        FontRepository.Substitutions.Add(
            new SimpleFontSubstitution("MyCustomFont", customFontPath)
        );

        using (Document htmlDoc = new Document(htmlPath, new HtmlLoadOptions()))
        {
            DocSaveOptions docOpts = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX // fonts are embedded automatically when possible
            };

            htmlDoc.Save(outputDocxPath, docOpts);
            Console.WriteLine($"HTML saved as DOCX with embedded fonts: {outputDocxPath}");
        }
    }
}
