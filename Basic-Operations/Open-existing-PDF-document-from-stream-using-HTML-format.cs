using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for HtmlSaveOptions (in Aspose.Pdf namespace)
 
class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF file
        const string htmlPath  = "output.html"; // destination HTML file
 
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }
 
        // Open the PDF from a FileStream (could be any Stream)
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream))   // load from stream
        {
            // Prepare HTML save options (required for non‑PDF output)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into a single HTML file (optional)
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Use PNG images for rasterized content (optional)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
 
            try
            {
                // Save the document as HTML
                doc.Save(htmlPath, htmlOpts);
                Console.WriteLine($"HTML file created: {htmlPath}");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}