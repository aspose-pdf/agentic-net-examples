using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class PdfProcessor
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_modified.pdf";
        const string outputHtmlPath = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Extract all text from the document using TextAbsorber
            // -----------------------------------------------------------------
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text;
            Console.WriteLine("Extracted text length: " + extractedText.Length);

            // -----------------------------------------------------------------
            // 2. Add a simple text annotation to the first page
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation and set its properties
            TextAnnotation textAnnot = new TextAnnotation(pdfDoc.Pages[1], annotRect)
            {
                Title    = "Note",
                Contents = "This is a sample text annotation added by Aspose.Pdf.",
                Open     = true,
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(textAnnot);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");

            // -----------------------------------------------------------------
            // 4. Convert the PDF to HTML – must pass HtmlSaveOptions explicitly
            // -----------------------------------------------------------------
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all resources (fonts, images) into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Save images as PNG embedded into SVG (cross‑platform safe)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            pdfDoc.Save(outputHtmlPath, htmlOptions);
            Console.WriteLine($"HTML version saved to '{outputHtmlPath}'.");
        }
    }
}