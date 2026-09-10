using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; the using block ensures Dispose() is called.
        using (Document doc = new Document(inputPath))
        {
            // ---------- Export to HTML ----------
            const string htmlPath = "output.html";
            using (FileStream htmlStream = new FileStream(htmlPath, FileMode.Create, FileAccess.Write))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                // Save to the stream with explicit options.
                doc.Save(htmlStream, htmlOpts);
            } // htmlStream is closed here.

            // ---------- Export first page to PNG ----------
            const string pngPath = "page1.png";
            using (FileStream pngStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
            {
                // PngDevice does NOT implement IDisposable, so instantiate it directly.
                PngDevice pngDevice = new PngDevice();
                pngDevice.Process(doc.Pages[1], pngStream);
            } // pngStream is closed here.

            // ---------- Export annotations to XFDF ----------
            const string xfdfPath = "annotations.xfdf";
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // ExportAnnotationsToXfdf writes XFDF data into the stream.
                doc.ExportAnnotationsToXfdf(xfdfStream);
            } // xfdfStream is closed here.
        } // Document is disposed here.

        Console.WriteLine("All export operations completed; streams have been closed.");
    }
}