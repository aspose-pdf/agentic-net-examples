using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string outputDocx = "output.docx";

        // Paths to custom font files that should be embedded in the DOCX
        string[] customFontFiles = { "fonts/CustomFont1.ttf", "fonts/CustomFont2.otf" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Register custom fonts via FontRepository.Substitutions so they can be used and embedded
        foreach (string fontPath in customFontFiles)
        {
            if (File.Exists(fontPath))
            {
                // Use the file name (without extension) as the font family name for substitution
                string fontFamily = Path.GetFileNameWithoutExtension(fontPath);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontFamily, fontPath));
            }
            else
            {
                Console.Error.WriteLine($"Font file not found: {fontPath}");
            }
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // ---------- Convert PDF to HTML ----------
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                pdfDoc.Save(outputHtml, htmlOpts);

                // ---------- Save PDF as DOCX with embedded fonts ----------
                // DocSaveOptions only requires the format; fonts are embedded automatically when registered
                DocSaveOptions docOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocx, docOpts);
            }

            Console.WriteLine($"Conversion completed. HTML saved to '{outputHtml}', DOCX saved to '{outputDocx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
