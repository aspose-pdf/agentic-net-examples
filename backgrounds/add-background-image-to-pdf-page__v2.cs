using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "background.jpg";
        const string outputPdf = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image bytes once so we can reuse the stream for both the page resources and the background artifact
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        using (var imgStreamForResources = new MemoryStream(imageBytes))
        using (var imgStreamForBackground = new MemoryStream(imageBytes))
        using (var doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Add the image to the page resources – this returns the image name (string)
            string imageName = page.Resources.Images.Add(imgStreamForResources);
            // (The returned name can be used with page.AddImage if needed, but is not required for the background artifact)

            // Create a BackgroundArtifact and set its BackgroundImage using a stream
            BackgroundArtifact bgArtifact = new BackgroundArtifact
            {
                BackgroundImage = imgStreamForBackground
            };

            // Attach the artifact to the first page
            page.Artifacts.Add(bgArtifact);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
