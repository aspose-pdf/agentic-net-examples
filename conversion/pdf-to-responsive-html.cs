using System;
using System.IO;
using Aspose.Pdf;

class PdfToResponsiveHtml
{
    static void Main()
    {
        // Paths for input PDF, output HTML and the responsive CSS file.
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string cssPath        = "responsive.css";

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Create a simple responsive CSS stylesheet.
        // In a real scenario you would replace this with your own stylesheet.
        string responsiveCss = @"
/* Mobile‑friendly responsive layout */
body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; }
img { max-width:100%; height:auto; }
@media only screen and (max-width:600px) {
    .page { width:100% !important; }
}
";
        try
        {
            File.WriteAllText(cssPath, responsiveCss);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSS file: {ex.Message}");
            return;
        }

        // Convert PDF to HTML using Aspose.Pdf.
        try
        {
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize HtmlSaveOptions.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed raster images into SVG to keep the HTML self‑contained.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Generate a single HTML file (no page splitting).
                    SplitIntoPages = false,
                    // Optional: set a title for the HTML page.
                    Title = Path.GetFileNameWithoutExtension(inputPdfPath)
                };

                // Save the PDF as HTML.
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and works only on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF‑to‑HTML conversion: {ex.Message}");
            return;
        }

        // Inject a link to the responsive CSS stylesheet into the generated HTML.
        try
        {
            string htmlContent = File.ReadAllText(outputHtmlPath);
            // Insert the <link> tag just before the closing </head> tag.
            string linkTag = $@"<link rel=""stylesheet"" type=""text/css"" href=""{Path.GetFileName(cssPath)}"">";
            if (htmlContent.Contains("</head>", StringComparison.OrdinalIgnoreCase))
            {
                htmlContent = htmlContent.Replace("</head>", $"{linkTag}</head>", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                // Fallback: prepend the link at the very beginning.
                htmlContent = linkTag + Environment.NewLine + htmlContent;
            }
            File.WriteAllText(outputHtmlPath, htmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to inject CSS into HTML: {ex.Message}");
            return;
        }

        Console.WriteLine($"PDF successfully converted to responsive HTML: {outputHtmlPath}");
    }
}