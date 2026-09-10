using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for Stamp if needed (optional)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string backgroundImg = "background.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImg))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImg}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages and add the background artifact
            foreach (Page page in pdfDoc.Pages)
            {
                // Create a background artifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact (the image will be tiled or stretched as needed)
                bgArtifact.SetImage(backgroundImg);

                // Place the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Optional: adjust opacity for a subtle effect (0.0 = fully transparent, 1.0 = opaque)
                bgArtifact.Opacity = 0.5; // 50% opacity

                // NOTE: Aspose.Pdf does not expose a direct BlendMode property for artifacts.
                // The visual effect can be approximated by adjusting opacity as shown above.

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background image saved to '{outputPdfPath}'.");
    }
}