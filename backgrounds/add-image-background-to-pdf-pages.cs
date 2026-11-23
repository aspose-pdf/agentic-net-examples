using System;
using System.Collections.Generic;
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

        // Load the image bytes once – we will create separate streams for each artifact.
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        // Keep the streams alive until the document is saved.
        var imageStreams = new List<MemoryStream>();

        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // ---------- First page ----------
            Page page1 = pdfDoc.Pages.Add();

            // Create a stream for the first background artifact.
            var bgStream1 = new MemoryStream(imageBytes);
            imageStreams.Add(bgStream1);
            var bgArtifact1 = new BackgroundArtifact
            {
                BackgroundImage = bgStream1,
                Opacity = 1.0
            };
            page1.Artifacts.Add(bgArtifact1);

            // ---------- Second page (reuse same background) ----------
            Page page2 = pdfDoc.Pages.Add();
            var bgStream2 = new MemoryStream(imageBytes);
            imageStreams.Add(bgStream2);
            var bgArtifact2 = new BackgroundArtifact
            {
                BackgroundImage = bgStream2,
                Opacity = 1.0
            };
            page2.Artifacts.Add(bgArtifact2);

            // Save the PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
