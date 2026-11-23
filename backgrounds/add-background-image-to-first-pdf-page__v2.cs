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
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image bytes once and keep the stream alive until the document is saved.
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Create a new PDF document.
            using (Document doc = new Document())
            {
                // ---------- Page 1 with background ----------
                Page page1 = doc.Pages.Add();

                // BackgroundArtifact expects a Stream for the background image.
                var bgArtifact = new BackgroundArtifact
                {
                    BackgroundImage = imageStream // assign the stream directly
                };
                page1.Artifacts.Add(bgArtifact);

                // ---------- Page 2 without background ----------
                doc.Pages.Add();

                // Save the PDF document.
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
