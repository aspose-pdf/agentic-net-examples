using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string textureImg = "texture.png"; // background texture image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(textureImg))
        {
            Console.Error.WriteLine($"Texture image not found: {textureImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Apply the background texture to each page
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact (placed behind page contents)
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact
                bgArtifact.SetImage(textureImg);

                // Mark it as a background element
                bgArtifact.IsBackground = true;

                // Adjust opacity to achieve a subtle overlay effect
                bgArtifact.Opacity = 0.5; // 0 = fully transparent, 1 = fully opaque

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background texture applied and saved to '{outputPdf}'.");
    }
}