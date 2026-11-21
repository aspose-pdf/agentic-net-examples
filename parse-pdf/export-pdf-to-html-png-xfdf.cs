using System;
using System.IO;
using Aspose.Pdf;

class ExportExample
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF; the using block ensures the Document is disposed.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Export to HTML (Windows only; wrap in try‑catch for cross‑platform safety).
            try
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                using (FileStream htmlStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
                {
                    pdfDoc.Save(htmlStream, htmlOpts);
                }
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML export requires Windows (GDI+). Skipped.");
            }

            // Export to SVG.
            using (FileStream svgStream = new FileStream("output.svg", FileMode.Create, FileAccess.Write))
            {
                SvgSaveOptions svgOpts = new SvgSaveOptions();
                pdfDoc.Save(svgStream, svgOpts);
            }

            // Export to DOCX.
            using (FileStream docxStream = new FileStream("output.docx", FileMode.Create, FileAccess.Write))
            {
                DocSaveOptions docOpts = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(docxStream, docOpts);
            }

            // Export to EPUB.
            using (FileStream epubStream = new FileStream("output.epub", FileMode.Create, FileAccess.Write))
            {
                EpubSaveOptions epubOpts = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                pdfDoc.Save(epubStream, epubOpts);
            }
        }

        Console.WriteLine("Export operations completed.");
    }
}