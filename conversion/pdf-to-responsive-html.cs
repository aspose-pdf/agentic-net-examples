using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Simple responsive CSS – you can replace this with a more elaborate stylesheet.
        const string responsiveCss = @"
            <style>
                body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; }
                img { max-width:100%; height:auto; }
                .page { width:100%; box-sizing:border-box; padding:10px; }
                @media (max-width:600px) {
                    .page { padding:5px; }
                }
            </style>";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options. GDI+ is required on non‑Windows platforms,
                // so we wrap the save in a try‑catch for TypeInitializationException.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed images as PNGs inside the HTML (cross‑platform friendly).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Do not split into separate HTML files per page.
                    SplitIntoPages = false
                };

                try
                {
                    // Save PDF as a single HTML file.
                    pdfDoc.Save(outputHtmlPath, htmlOptions);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
                    return;
                }
            }

            // At this point the HTML file exists. Insert the responsive CSS.
            string htmlContent = File.ReadAllText(outputHtmlPath);

            // Find the closing </head> tag to inject the CSS before it.
            int headCloseIndex = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headCloseIndex >= 0)
            {
                htmlContent = htmlContent.Insert(headCloseIndex, responsiveCss);
            }
            else
            {
                // If no </head> tag is present, prepend the CSS at the start of the file.
                htmlContent = responsiveCss + htmlContent;
            }

            // Write the modified HTML back to disk.
            File.WriteAllText(outputHtmlPath, htmlContent);

            Console.WriteLine($"PDF successfully converted to HTML with responsive CSS: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
