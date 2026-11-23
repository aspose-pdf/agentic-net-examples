using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath = "background.jpg";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Open the image stream once and keep it alive until the document is saved
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            using (Document doc = new Document())
            {
                // Ensure the document has at least five pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 0; i < 5; i++)
                    doc.Pages.Add();

                // Retrieve page five
                Page pageFive = doc.Pages[5];

                // Create the background artifact and set its BackgroundImage property to the stream
                BackgroundArtifact bgArtifact = new BackgroundArtifact
                {
                    BackgroundImage = imgStream
                };

                // Add the artifact to the page's artifact collection
                pageFive.Artifacts.Add(bgArtifact);

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
