using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // BlendMode enum (may be unused if not supported)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF (may not exist)
        const string outputPdf = "output.pdf"; // result PDF
        const string texture = "texture.png"; // background texture image

        // Ensure a source PDF exists – create a simple one if it does not.
        Document doc;
        if (File.Exists(inputPdf))
        {
            doc = new Document(inputPdf);
        }
        else
        {
            // Create a one‑page blank document as a fallback.
            doc = new Document();
            doc.Pages.Add();
        }

        // Verify that the texture image exists – otherwise skip adding the background.
        bool textureExists = File.Exists(texture);
        if (!textureExists)
        {
            Console.WriteLine($"Warning: texture file '{texture}' not found. Background will not be added.");
        }

        // Iterate over all pages (1‑based indexing)
        foreach (Page page in doc.Pages)
        {
            if (!textureExists) continue; // nothing to add

            // Create a background artifact for the page
            BackgroundArtifact bg = new BackgroundArtifact();

            // Set the texture image (generator‑only property)
            // Using SetImage(string) overload – it loads the image from file.
            bg.SetImage(texture);

            // Place the artifact behind page contents
            bg.IsBackground = true;

            // Use partial opacity to achieve a subtle effect
            bg.Opacity = 0.5; // adjust as needed for subtlety

            // Set blend mode to Overlay for texture‑like appearance (if supported)
            // The BlendMode property is available in newer versions of Aspose.PDF.
            // If the property does not exist in the referenced version, the line can be omitted.
            // bg.BlendMode = BlendMode.Overlay;

            // Add the artifact to the page
            page.Artifacts.Add(bg);
        }

        // Save the modified PDF
        doc.Save(outputPdf);
        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
