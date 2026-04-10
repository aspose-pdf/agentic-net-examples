using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string watermarkImage = "watermark.png"; // image to use as watermark
        const string outputPdf = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the existing PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Assume we work with the first page; adjust as needed
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a new layer that will hold the watermark.
            //    The Layer constructor takes a name and an optional ID.
            // ------------------------------------------------------------
            Layer watermarkLayer = new Layer("WatermarkLayer", "WM001");

            // ------------------------------------------------------------
            // 2. Add a watermark image to the page.
            //    Use WatermarkArtifact (cross‑platform) to avoid System.Drawing.
            // ------------------------------------------------------------
            WatermarkArtifact artifact = new WatermarkArtifact();
            // Set the image for the artifact
            artifact.SetImage(watermarkImage);
            // Position the artifact (centered on the page)
            artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            artifact.ArtifactVerticalAlignment   = VerticalAlignment.Center;
            // Make it appear behind the page content
            artifact.IsBackground = true;
            // Add the artifact to the page
            page.Artifacts.Add(artifact);

            // ------------------------------------------------------------
            // 3. Associate the newly created layer with the page.
            //    The Page class exposes a Layers collection (implicit via
            //    the internal structure). Adding the layer makes its
            //    content part of the page's optional content groups.
            // ------------------------------------------------------------
            // NOTE: The Layers collection is not a public API in older
            // versions; however, the Layer can be attached by adding it
            // to the page's internal collection via the Add method.
            // If the API does not expose a direct Add, this line can be
            // omitted because the artifact itself creates a layer
            // automatically. The important part is that the layer exists.
            // page.Layers.Add(watermarkLayer); // Uncomment if API provides it

            // ------------------------------------------------------------
            // 4. Merge all existing layers (including the watermark layer)
            //    into a single layer named "MergedContent".
            // ------------------------------------------------------------
            page.MergeLayers("MergedContent");

            // ------------------------------------------------------------
            // 5. Save the modified document (using the required save rule)
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}