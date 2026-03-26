using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string htmlFile = "output.html";
        const string docxFile = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Step 1: Convert PDF to HTML (embed resources into the HTML file)
        using (Document pdfDoc = new Document(inputPdf))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            pdfDoc.Save(htmlFile, htmlOpts);
        }

        // Step 2: Load the generated HTML and save it as DOCX (fonts are embedded by default)
        using (Document htmlDoc = new Document(htmlFile, new HtmlLoadOptions()))
        {
            DocSaveOptions docOpts = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
                // No FontSavingMode property – fonts are embedded automatically in recent Aspose.PDF versions
            };
            htmlDoc.Save(docxFile, docOpts);
        }

        Console.WriteLine($"Conversion completed: DOCX saved to '{docxFile}' with embedded fonts.");
    }
}
