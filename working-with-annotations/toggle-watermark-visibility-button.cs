using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
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

        // Load the PDF, modify it, and save it.
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a WatermarkArtifact and add it to the page.
            // ------------------------------------------------------------
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                // Example: set some visual properties.
                Text = "CONFIDENTIAL",
                TextState = new TextState
                {
                    FontSize = 72,
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    ForegroundColor = Aspose.Pdf.Color.Red,
                    BackgroundColor = Aspose.Pdf.Color.Transparent
                },
                // Position the watermark in the centre of the page.
                Position = new Point(page.PageInfo.Width / 2, page.PageInfo.Height / 2),
                // Initially visible.
                IsBackground = false,
                Opacity = 0.5
            };
            page.Artifacts.Add(watermark);

            // ------------------------------------------------------------
            // 2. Create a push button that toggles the watermark visibility.
            // ------------------------------------------------------------
            // Define button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(50, 50, 150, 80);
            ButtonField toggleButton = new ButtonField(page, btnRect)
            {
                // Caption shown on the button.
                NormalCaption = "Toggle Watermark",
                // Optional visual styling – set after construction because Border needs the instance.
                Color = Aspose.Pdf.Color.LightGray
            };
            // Set the border now that the instance exists.
            toggleButton.Border = new Border(toggleButton) { Width = 1 };

            // JavaScript action to toggle the artifact's opacity between 0 (hidden) and 0.5 (visible).
            // The script accesses the first artifact on the page (index 0) and flips its opacity.
            string js = @"
var art = this.getPageNth(0).artifacts[0];
if (art.opacity == 0) {
    art.opacity = 0.5;
} else {
    art.opacity = 0;
}
";
            // Assign the JavaScript action to the button's mouse‑release event.
            toggleButton.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // Add the button to the page's annotations collection.
            page.Annotations.Add(toggleButton);

            // ------------------------------------------------------------
            // Save the modified PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'.");
    }
}
