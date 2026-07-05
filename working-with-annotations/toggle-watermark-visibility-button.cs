using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

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

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Create a WatermarkArtifact and add it to the page
            // ------------------------------------------------------------
            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.Text = "CONFIDENTIAL";
            watermark.TextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Color.Red,
                BackgroundColor = Color.Transparent
            };
            watermark.Opacity = 0.5;          // semi‑transparent
            watermark.IsBackground = true;   // placed behind page content
            watermark.LeftMargin = 100;      // position via margins
            watermark.TopMargin = 500;
            page.Artifacts.Add(watermark);

            // ------------------------------------------------------------
            // Create a push button that toggles the watermark visibility
            // ------------------------------------------------------------
            // Define button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);
            ButtonField toggleBtn = new ButtonField(page, btnRect);
            toggleBtn.Name = "ToggleWatermark";
            toggleBtn.AlternateCaption = "Hide";
            toggleBtn.NormalCaption = "Show";

            // JavaScript action: flip the IsBackground flag of the first artifact
            // and update the button caption accordingly.
            string js = @"
var pg = this.getPageNum() - 1;
var art = this.getPageNth(pg).artifacts[0];
if (art != null) {
    art.isBackground = !art.isBackground;
    var btn = this.getField('ToggleWatermark');
    btn.value = (art.isBackground ? 'Hide' : 'Show');
}
";
            toggleBtn.OnActivated = new JavascriptAction(js);

            // Add the button to the page annotations collection
            page.Annotations.Add(toggleBtn);

            // ------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: using block)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}