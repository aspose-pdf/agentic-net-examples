using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, SaveOptions, etc.)
using Aspose.Pdf.Text;          // Required for text‑related options (if needed)

public class ExportWithStreamCleanup
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string pdfOutput = "output.pdf";
        const string htmlOutput = "output.html";
        const string svgOutput = "output.svg";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block ensures Dispose() is called
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Export to PDF (same format) – simple Save to file path
            // -----------------------------------------------------------------
            doc.Save(pdfOutput);   // No stream involved; Document will be disposed later

            // -----------------------------------------------------------------
            // 2. Export to HTML – must pass HtmlSaveOptions explicitly
            //    Use a FileStream and close it after the Save call
            // -----------------------------------------------------------------
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            using (FileStream htmlStream = new FileStream(htmlOutput, FileMode.Create, FileAccess.Write))
            {
                doc.Save(htmlStream, htmlOpts);   // Stream is written here
            }   // htmlStream.Dispose() – stream closed and resources released

            // -----------------------------------------------------------------
            // 3. Export to SVG – must pass SvgSaveOptions explicitly
            //    Use a FileStream and close it after the Save call
            // -----------------------------------------------------------------
            SvgSaveOptions svgOpts = new SvgSaveOptions();

            using (FileStream svgStream = new FileStream(svgOutput, FileMode.Create, FileAccess.Write))
            {
                doc.Save(svgStream, svgOpts);    // Stream is written here
            }   // svgStream.Dispose() – stream closed and resources released
        }   // Document.Dispose() – all internal resources released
    }
}