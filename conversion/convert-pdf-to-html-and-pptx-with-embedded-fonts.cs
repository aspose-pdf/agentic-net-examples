using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string pptxPath = "presentation.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to HTML using core Aspose.Pdf API
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                pdfDoc.Save(htmlPath, htmlOptions);
                Console.WriteLine($"PDF converted to HTML: {htmlPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting PDF to HTML: {ex.Message}");
            return;
        }

        // Convert PDF directly to PPTX. Aspose.Pdf embeds the required fonts during conversion.
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                pdfDoc.Save(pptxPath, SaveFormat.Pptx);
                Console.WriteLine($"PDF converted to PPTX: {pptxPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting PDF to PPTX: {ex.Message}");
        }
    }
}
