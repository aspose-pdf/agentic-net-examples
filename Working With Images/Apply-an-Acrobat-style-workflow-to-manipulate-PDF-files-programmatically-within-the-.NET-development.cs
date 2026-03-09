using System;
using System.IO;
using System.Drawing; // Added for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class AcrobatStyleWorkflow
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_acrobat_style.pdf";
        const string outputHtmlPath = "output_acrobat_style.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Update document metadata (Title, Author, Language)
            // ------------------------------------------------------------
            pdfDoc.Info.Title  = "Acrobat‑Style Processed PDF";
            pdfDoc.Info.Author = "AcrobatStyleWorkflow";

            // Language is stored in tagged content; set it if the document is tagged
            ITaggedContent tagged = pdfDoc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(pdfDoc.Info.Title);

            // ------------------------------------------------------------
            // 2. Add a free‑text annotation (similar to Acrobat's "Add Text")
            // ------------------------------------------------------------
            // Use fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle freeTextRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            // DefaultAppearance expects System.Drawing.Color for TextColor
            DefaultAppearance appearance = new DefaultAppearance();
            appearance.FontSize = 12;
            appearance.TextColor = System.Drawing.Color.Blue; // Fixed type conversion

            FreeTextAnnotation freeText = new FreeTextAnnotation(pdfDoc.Pages[1], freeTextRect, appearance)
            {
                Contents = "Added by Acrobat‑Style workflow",
                Color    = Aspose.Pdf.Color.Yellow, // annotation background
                Opacity  = 0.5,
                // Optional callout line (3 points required)
                Callout = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(150, 620), // start inside annotation
                    new Aspose.Pdf.Point(200, 680), // knee
                    new Aspose.Pdf.Point(250, 720)  // end (pointing to target)
                }
            };
            pdfDoc.Pages[1].Annotations.Add(freeText);

            // ------------------------------------------------------------
            // 3. Add a link annotation to an external URL (Acrobat's "Add Link")
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 540);
            LinkAnnotation link = new LinkAnnotation(pdfDoc.Pages[1], linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,
                // Use GoToURIAction for external URLs
                Action = new GoToURIAction("https://www.example.com")
            };
            pdfDoc.Pages[1].Annotations.Add(link);

            // ------------------------------------------------------------
            // 4. Encrypt the document (Acrobat's "Protect with Password")
            // ------------------------------------------------------------
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdfDoc.Encrypt(
                userPassword:  "user123",
                ownerPassword: "owner123",
                permissions:   perms,
                cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // ------------------------------------------------------------
            // 5. Optimize for web (Acrobat's "Optimize PDF")
            // ------------------------------------------------------------
            pdfDoc.Optimize();

            // ------------------------------------------------------------
            // 6. Save the modified PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);

            // ------------------------------------------------------------
            // 7. Save a copy as HTML (requires explicit HtmlSaveOptions)
            // ------------------------------------------------------------
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into the HTML file (similar to Acrobat's export)
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Render images as PNG inside SVG to avoid GDI+ issues on non‑Windows platforms
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            pdfDoc.Save(outputHtmlPath, htmlOpts);
        }

        Console.WriteLine($"Processing complete. PDF saved to '{outputPdfPath}', HTML saved to '{outputHtmlPath}'.");
    }
}
