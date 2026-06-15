using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for potential future extensions

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_background.pdf";
        const string bgImagePath = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages to add the background image
            foreach (Page page in doc.Pages)
            {
                // Create a BackgroundArtifact – this places content behind page elements
                BackgroundArtifact bgArtifact = new BackgroundArtifact
                {
                    IsBackground = true,          // ensure it is rendered behind page content
                    Opacity = 0.5f               // subtle shading; blend mode "multiply" is not directly exposed
                };

                // Load the image into the artifact
                bgArtifact.SetImage(bgImagePath);

                // Optionally position the artifact (full‑page coverage)
                // If Position is not set, the artifact will fill the page automatically.
                // For explicit positioning you could use:
                // bgArtifact.Position = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with background image: {outputPdf}");
    }
}