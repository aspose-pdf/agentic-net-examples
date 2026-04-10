using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bgImage = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages and add the background artifact
            foreach (Page page in doc.Pages)
            {
                // Create a BackgroundArtifact (read‑only Rectangle, set via methods)
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact
                using (FileStream imgStream = File.OpenRead(bgImage))
                {
                    bgArtifact.SetImage(imgStream);
                }

                // Place the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Optional: set opacity for a subtle effect (0..1)
                bgArtifact.Opacity = 0.7f;

                // NOTE: The core Aspose.Pdf API does not expose a BlendMode property for artifacts.
                // If a multiply blend effect is required, it must be achieved by pre‑processing the
                // background image (e.g., applying a multiply blend in an image editor) before
                // embedding it, or by using the Aspose.Pdf.Facades namespace which is prohibited
                // by the task constraints.

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image added. Saved to '{outputPdf}'.");
    }
}
