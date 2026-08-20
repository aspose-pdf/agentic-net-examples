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
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify, and save – wrapped in using for proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a WatermarkArtifact and add it to the page.
            // ------------------------------------------------------------
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the watermark text and its visual style.
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Color.Red
            };
            watermark.SetTextAndState("CONFIDENTIAL", ts);

            // Position the watermark (coordinates are in points).
            // WatermarkArtifact.Position expects a Point, not a Position.
            watermark.Position = new Point(100, 400);

            // Make the watermark semi‑transparent and place it behind page content.
            watermark.Opacity = 0.5;
            watermark.IsBackground = true;

            // Add the artifact to the page.
            page.Artifacts.Add(watermark);

            // ------------------------------------------------------------
            // 2. Add a push button that toggles the watermark visibility.
            // ------------------------------------------------------------
            // Define the button rectangle (llx, lly, urx, ury).
            Rectangle buttonRect = new Rectangle(50, 50, 150, 80);
            ButtonField toggleButton = new ButtonField(page, buttonRect)
            {
                // Text shown on the button.
                Contents = "Toggle Watermark"
            };

            // JavaScript to toggle the artifact's IsBackground flag.
            // Use a valid action property from AnnotationActionCollection.
            string js = @"
                var art = this.getPage(0).Artifacts[0];
                art.IsBackground = !art.IsBackground;
                this.getPage(0).update();
            ";
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the page's annotation collection.
            page.Annotations.Add(toggleButton);

            // ------------------------------------------------------------
            // Save the modified PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'.");
    }
}
