using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_background.pdf";
        const string bgImagePath = "texture.png"; // subtle texture image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // ----- Approach 1: use Page.BackgroundImage (generator only) -----
                // This sets the background image for the page. It is not read when loading an existing PDF,
                // but it will be written to the output PDF.
                Aspose.Pdf.Image bgImg = new Aspose.Pdf.Image();
                bgImg.File = bgImagePath;
                page.BackgroundImage = bgImg;

                // ----- Approach 2: use a BackgroundArtifact -----
                // Adding a BackgroundArtifact gives more control (e.g., opacity, alignment).
                // The artifact is placed behind page contents (IsBackground = true).
                // Blend mode "Overlay" is not directly exposed in Aspose.Pdf; using opacity can simulate a subtle effect.
                BackgroundArtifact artifact = new BackgroundArtifact();
                artifact.IsBackground = true;          // place behind page contents
                artifact.Opacity = 0.5;                // semi‑transparent for subtle overlay effect
                artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                artifact.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                // Set the image for the artifact
                using (FileStream imgStream = File.OpenRead(bgImagePath))
                {
                    artifact.SetImage(imgStream);
                }

                // Add the artifact to the page
                page.Artifacts.Add(artifact);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background texture: {outputPdf}");
    }
}