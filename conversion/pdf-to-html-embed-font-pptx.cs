using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // ---------- Convert PDF to HTML with embedded fonts ----------
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all resources (including fonts) into the single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Save fonts in TrueType format to ensure they are usable in browsers
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsTTF,
                // Optional: embed images as base64 to keep a single file
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };

            pdfDoc.Save(outputHtml, htmlOptions);
            Console.WriteLine($"HTML with embedded fonts saved to '{outputHtml}'.");

            // ---------- Save the same PDF as a PPTX presentation ----------
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);
            Console.WriteLine($"PPTX presentation saved to '{outputPptx}'.");
        }
    }
}