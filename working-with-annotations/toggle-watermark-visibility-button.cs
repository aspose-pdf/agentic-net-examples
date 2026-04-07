using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Create a WatermarkArtifact and add it to the page
            // ------------------------------------------------------------
            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.Text = "Sample Watermark";          // set watermark text
            watermark.IsBackground = true;                // place behind page content
            watermark.Opacity = 0.5;                      // semi‑transparent
            page.Artifacts.Add(watermark);                // add artifact to the page

            // ------------------------------------------------------------
            // Create a push button that will toggle the watermark visibility
            // ------------------------------------------------------------
            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            ButtonField toggleButton = new ButtonField(page, btnRect);
            toggleButton.Name = "ToggleWatermark";
            toggleButton.AlternateCaption = "Toggle Watermark";

            // JavaScript action to toggle the artifact's visibility.
            // The script checks the current opacity and switches between 0 (hidden) and 0.5 (visible).
            // Note: This script runs in the PDF viewer; actual artifact manipulation may depend on viewer support.
            string js = @"
                var art = this.getPageNthWord(0,0); // placeholder – obtain reference to the artifact if supported
                if (art != null) {
                    if (art.opacity == 0) {
                        art.opacity = 0.5;
                    } else {
                        art.opacity = 0;
                    }
                }
            ";
            toggleButton.OnActivated = new JavascriptAction(js);

            // Add the button to the page annotations collection
            page.Annotations.Add(toggleButton);

            // ------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use Document.Save)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}