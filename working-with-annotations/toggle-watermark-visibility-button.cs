using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction, WatermarkAnnotation
using Aspose.Pdf.Forms;        // ButtonField
using Aspose.Pdf.Text;          // TextState, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Create a WatermarkAnnotation and add it to the page
            // ------------------------------------------------------------
            var wmRect = new Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);
            var watermark = new WatermarkAnnotation(page, wmRect)
            {
                Name = "MyWatermark",
                Opacity = 0.3
                // IsBackground property was removed in recent SDK versions; the watermark is added as a foreground annotation.
            };
            // Set the watermark text and its style
            watermark.SetTextAndState(
                new[] { "CONFIDENTIAL" },
                new TextState
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 72,
                    ForegroundColor = Color.FromRgb(1, 0, 0) // Red
                });
            page.Annotations.Add(watermark);

            // ------------------------------------------------------------
            // Create a button field that will toggle the watermark
            // ------------------------------------------------------------
            // Rectangle for the button (coordinates are in points)
            var btnRect = new Rectangle(100, 500, 200, 550);

            var button = new ButtonField(page, btnRect)
            {
                PartialName = "ToggleWatermarkBtn",
                // DefaultAppearance must be set before setting the caption
                DefaultAppearance = new DefaultAppearance("Helvetica", 10, System.Drawing.Color.Black)
            };
            // Set the visible caption of the button
            button.NormalCaption = "Toggle Watermark";

            // JavaScript that toggles the visibility of the watermark annotation by name
            string js = @"
                var annots = this.getAnnots();
                for (var i = 0; i < annots.length; i++) {
                    if (annots[i].name == 'MyWatermark') {
                        annots[i].setVisible(!annots[i].isVisible());
                        break;
                    }
                }";

            button.OnActivated = new JavascriptAction(js);

            // Add the button to the document's AcroForm on page 1
            doc.Form.Add(button, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}
