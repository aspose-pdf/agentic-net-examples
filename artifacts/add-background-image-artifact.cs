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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // BackgroundArtifact has only a parameter‑less constructor.
            // The image must be supplied via the BackgroundImage property as a stream.
            byte[] imageBytes = File.ReadAllBytes(bgImage);
            using (var imageStream = new MemoryStream(imageBytes))
            {
                var background = new BackgroundArtifact
                {
                    BackgroundImage = imageStream,
                    Opacity = 1.0 // optional – set desired opacity (0.0‑1.0)
                };

                page.Artifacts.Add(background);

                // Save the modified PDF while the image stream is still alive
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with background artifact to '{outputPdf}'.");
    }
}
