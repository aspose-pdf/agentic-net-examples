using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a WatermarkArtifact and add it to the page
            // ------------------------------------------------------------
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                Text = "CONFIDENTIAL",
                // Position the watermark (center of the page)
                Position = new Point(page.PageInfo.Width / 2, page.PageInfo.Height / 2),
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment = VerticalAlignment.Center,
                Opacity = 1.0
            };
            page.Artifacts.Add(watermark);

            // ------------------------------------------------------------
            // 2. Create a push button that will toggle the watermark visibility
            // ------------------------------------------------------------
            // Define button rectangle (lower‑left corner at (50,50), size 100x30)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(50, 50, 150, 80);
            ButtonField toggleButton = new ButtonField(page, btnRect);
            toggleButton.NormalCaption = "Toggle Watermark";
            // Set a partial name (optional but recommended)
            toggleButton.PartialName = "toggleWatermarkBtn";
            // Configure a simple border (no color property on Border class)
            toggleButton.Border = new Border(toggleButton) { Width = 1 };
            // Optional: set border color via the annotation's own Color property
            toggleButton.Color = Color.Black;

            // ------------------------------------------------------------
            // 3. Attach a JavaScript action that toggles the artifact's opacity
            // ------------------------------------------------------------
            string js = @"
var pageNum = this.getPageNum();
var art = this.getPageNthArtifact(pageNum - 1, 0);
if (art.opacity == 1) {
    art.opacity = 0;
} else {
    art.opacity = 1;
}
this.dirty = true;
";
            // Use a valid action property for button clicks
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the page annotations collection
            page.Annotations.Add(toggleButton);

            // ------------------------------------------------------------
            // 4. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'.");
    }
}
