using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (repeat for other pages if needed)
            Page page = doc.Pages[1];

            // Define a rectangle that covers the whole page
            double llx = 0;
            double lly = 0;
            double urx = page.PageInfo.Width;
            double ury = page.PageInfo.Height;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // ---------------------------------------------------------------------
            // NOTE: WatermarkAnnotation does not expose a Rotate property and does not
            // support arbitrary rotation or gradient fills directly. To achieve a
            // diagonal, semi‑transparent watermark we use a WatermarkArtifact, which
            // provides Rotation (any angle) and Opacity. Gradient fill is not
            // available in the current Aspose.PDF API, so we use a solid colour with
            // the desired opacity.
            // ---------------------------------------------------------------------
            var watermark = new WatermarkArtifact
            {
                Text = "CONFIDENTIAL",
                // TextState defines font, size and colour
                TextState = new TextState
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 72,
                    ForegroundColor = Color.Gray
                },
                // Position the artifact in the centre of the page
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment = VerticalAlignment.Center,
                // Rotate 45 degrees to make it diagonal
                Rotation = 45,
                // Semi‑transparent
                Opacity = 0.3,
                // Make it a background artifact so it appears behind page content
                IsBackground = true
            };

            // Add the artifact to the page
            page.Artifacts.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
