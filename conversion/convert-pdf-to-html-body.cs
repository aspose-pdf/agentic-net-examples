using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output_body.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Flow layout, single file, only <body> markup.
                    FixedLayout = false,
                    SplitIntoPages = false,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Embed raster images as PNG inside SVG to keep output lightweight.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML body saved to '{outputHtml}'.");
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}