using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for BlendMode enum (if available)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a background artifact for the first page
            BackgroundArtifact bgArtifact = new BackgroundArtifact();

            // Set the image for the background artifact
            bgArtifact.SetImage(imagePath);

            // Mark the artifact as a background (placed behind page contents)
            bgArtifact.IsBackground = true;

            // OPTIONAL: Set blend mode to Multiply for subtle shading.
            // The BlendMode property exists on Artifact (or its derived types) in newer versions.
            // If the property is not available in your version, you may need to adjust opacity instead.
            // Uncomment the following line if BlendMode is supported:
            // bgArtifact.BlendMode = BlendMode.Multiply;

            // Add the artifact to the page's artifact collection
            // (Page indices are 1‑based in Aspose.Pdf)
            doc.Pages[1].Artifacts.Add(bgArtifact);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image added and saved to '{outputPdf}'.");
    }
}