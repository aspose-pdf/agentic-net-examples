using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "intermediate.html";
        const string docxPath = "output.docx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Convert PDF -> HTML with fonts saved in all formats
            // ------------------------------------------------------------
            using (var pdfDoc = new Document(pdfPath))
            {
                var htmlOpts = new HtmlSaveOptions
                {
                    FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    DefaultFontName = "Arial"
                };
                pdfDoc.Save(htmlPath, htmlOpts);
            }

            // ------------------------------------------------------------
            // Step 2: Load the generated HTML and embed fonts into DOCX
            // ------------------------------------------------------------
            var loadOpts = new HtmlLoadOptions { IsEmbedFonts = true };
            using (var htmlDoc = new Document(htmlPath, loadOpts))
            {
                var docOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX,
                    ReSaveFonts = true,
                    ConvertType3Fonts = true
                };
                htmlDoc.Save(docxPath, docOpts);
            }

            Console.WriteLine($"Conversion complete. DOCX saved to '{docxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
