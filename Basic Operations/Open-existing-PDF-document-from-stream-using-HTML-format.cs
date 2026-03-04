using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // not needed here but safe for completeness

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF file
        const string htmlOutput = "output.html";    // desired HTML file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF from a FileStream (could be any Stream source)
        using (FileStream pdfStream = File.OpenRead(pdfPath))
        using (Document doc = new Document(pdfStream)) // lifecycle: using ensures disposal
        {
            // Prepare HTML save options (required for non‑PDF output)
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Example settings – adjust as needed
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // HTML conversion relies on GDI+; handle possible platform limitation
            try
            {
                doc.Save(htmlOutput, htmlOptions);
                Console.WriteLine($"HTML saved to '{htmlOutput}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML save: {ex.Message}");
            }
        }
    }
}