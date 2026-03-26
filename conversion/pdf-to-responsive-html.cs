using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string cssFile = "responsive.css";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Write a simple responsive CSS file.
        string cssContent = @"
body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; }
img { max-width:100%; height:auto; }
@media only screen and (max-width:600px) {
    .page { width:100% !important; }
}
";
        File.WriteAllText(cssFile, cssContent);

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    // Do not embed CSS or images; they will be saved as external files.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Save raster images as external PNG files referenced via SVG (so they stay separate).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Optional: keep the whole document in a single HTML file (no page splitting).
                    SplitIntoPages = false
                };

                pdfDoc.Save(outputHtml, saveOptions);
            }

            // Inject a link to the responsive stylesheet into the generated HTML.
            string html = File.ReadAllText(outputHtml);
            int headClose = html.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headClose >= 0)
            {
                string linkTag = $"<link rel=\"stylesheet\" href=\"{cssFile}\" />{Environment.NewLine}";
                html = html.Insert(headClose, linkTag);
                File.WriteAllText(outputHtml, html);
            }

            Console.WriteLine($"PDF converted to HTML with responsive CSS: {outputHtml}");
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
