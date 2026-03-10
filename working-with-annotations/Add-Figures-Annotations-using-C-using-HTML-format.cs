using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create a concrete figure annotation (SquareAnnotation)
                SquareAnnotation square = new SquareAnnotation(page, rect)
                {
                    Color = Aspose.Pdf.Color.Blue,   // Border color
                    Contents = "Figure annotation",   // Tooltip text
                    Title = "Figure"                  // Title shown in the popup
                };

                // Add the annotation to the page
                page.Annotations.Add(square);

                // Save the modified document as HTML using explicit HtmlSaveOptions
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                doc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"HTML saved to '{outputHtml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}