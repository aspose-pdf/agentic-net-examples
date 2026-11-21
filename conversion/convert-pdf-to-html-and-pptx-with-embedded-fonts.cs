using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions and SaveFormat are in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string htmlPath = "output.html";
        const string pptxPath = "output.pptx";

        // ---------- Convert PDF to HTML (Windows‑only, GDI+ required) ----------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfPath))
        {
            var htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                pdfDoc.Save(htmlPath, htmlOpts);
                Console.WriteLine($"PDF → HTML saved to '{htmlPath}'.");
            }
            catch (TypeInitializationException)
            {
                // GDI+ not available (non‑Windows platform)
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML export.");
            }

            // ---------- Convert PDF directly to PPTX ----------
            // Aspose.Pdf can save a PDF document as PPTX. Custom font embedding for PPTX
            // is not supported without Aspose.Slides, so we perform a straightforward conversion.
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
            Console.WriteLine($"PDF → PPTX saved to '{pptxPath}'.");
        }
    }
}
