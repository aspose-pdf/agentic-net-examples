using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Export the whole PDF to HTML using a FileStream
            using (FileStream htmlStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                doc.Save(htmlStream, htmlOpts);
            } // htmlStream is closed here

            // Export the first page as a PNG image using a FileStream
            using (FileStream imgStream = new FileStream("page1.png", FileMode.Create, FileAccess.Write))
            {
                // Use PngDevice instead of ImageSaveOptions (ImageSaveOptions is not available in current Aspose.PDF API)
                var pngDevice = new PngDevice(new Resolution(300));
                // Pages collection is 1‑based, so page 1 is doc.Pages[1]
                pngDevice.Process(doc.Pages[1], imgStream);
            } // imgStream is closed here

            // Export all annotations to an XFDF file using a FileStream
            using (FileStream xfdfStream = new FileStream("annotations.xfdf", FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            } // xfdfStream is closed here
        } // Document is disposed here
    }
}