using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // Required for HtmlSaveOptions (in Aspose.Pdf namespace)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // ---------- Export to PDF (copy) ----------
            const string pdfOutput = "output_copy.pdf";
            using (FileStream pdfStream = new FileStream(pdfOutput, FileMode.Create, FileAccess.Write))
            {
                // Save the document as PDF into the stream
                doc.Save(pdfStream);
            } // pdfStream.Dispose() called here

            // ---------- Export to HTML ----------
            const string htmlOutput = "output.html";
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            using (FileStream htmlStream = new FileStream(htmlOutput, FileMode.Create, FileAccess.Write))
            {
                // Save the document as HTML using explicit HtmlSaveOptions
                doc.Save(htmlStream, htmlOpts);
            } // htmlStream.Dispose() called here

            // ---------- Export to XML ----------
            const string xmlOutput = "output.xml";
            using (FileStream xmlStream = new FileStream(xmlOutput, FileMode.Create, FileAccess.Write))
            {
                // Save the document as XML using the SaveFormat enum
                doc.Save(xmlStream, SaveFormat.Xml);
            } // xmlStream.Dispose() called here
        } // doc.Dispose() called here

        Console.WriteLine("All export operations completed successfully.");
    }
}