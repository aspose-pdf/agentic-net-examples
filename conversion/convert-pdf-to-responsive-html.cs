using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";
        // Output HTML path
        const string outputHtml = "output.html";
        // Path to the responsive CSS file that will be linked from the HTML
        const string cssFile = "responsive.css";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure a responsive CSS file exists; create a simple one if missing
        if (!File.Exists(cssFile))
        {
            File.WriteAllText(cssFile,
@"/* Simple responsive CSS */
body { margin:0; padding:0; font-family:Arial,Helvetica,sans-serif; }
img { max-width:100%; height:auto; }
@media only screen and (max-width:600px) {
    .content { padding:10px; }
}");
        }

        // Convert PDF to HTML using HtmlSaveOptions (required explicit options)
        using (Document pdfDoc = new Document(inputPdf))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

            // Use flow layout (FixedLayout = false) for better responsiveness
            htmlOpts.FixedLayout = false;

            // Embed raster images as PNG inside SVG to keep a single HTML file
            htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

            // Optional: set a page title
            htmlOpts.Title = "Converted HTML";

            // Save the HTML file
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        // After conversion, inject a <link> tag that references the responsive CSS
        if (File.Exists(outputHtml))
        {
            string htmlContent = File.ReadAllText(outputHtml);
            string linkTag = $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Path.GetFileName(cssFile)}\" />{Environment.NewLine}";

            // Insert the link before the closing </head> tag (case‑insensitive search)
            int headCloseIdx = htmlContent.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headCloseIdx >= 0)
            {
                htmlContent = htmlContent.Insert(headCloseIdx, linkTag);
                File.WriteAllText(outputHtml, htmlContent);
                Console.WriteLine($"HTML saved to '{outputHtml}' with responsive CSS linked.");
            }
            else
            {
                Console.Error.WriteLine("Unable to locate </head> tag in the generated HTML.");
            }
        }
        else
        {
            Console.Error.WriteLine("HTML conversion failed; output file not found.");
        }
    }
}