using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath  = "background.jpg";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image into a MemoryStream that will stay alive until the document is saved
        byte[] imgBytes = File.ReadAllBytes(imagePath);
        MemoryStream imgStream = new MemoryStream(imgBytes);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Aspose.Pdf creates a first page automatically; retrieve it (1‑based index)
            Page page = doc.Pages[1];

            // Create a BackgroundArtifact and assign the image stream to BackgroundImage
            BackgroundArtifact bgArtifact = new BackgroundArtifact
            {
                BackgroundImage = imgStream
            };

            page.Artifacts.Add(bgArtifact);

            // Save the PDF document
            doc.Save(outputPath);
        }

        // Dispose the image stream after the document has been saved
        imgStream.Dispose();

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
