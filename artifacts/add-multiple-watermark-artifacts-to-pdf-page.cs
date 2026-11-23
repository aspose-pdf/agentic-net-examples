using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextState, FontRepository, Position, Point

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain page 5.");
                return;
            }

            // Get page five (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[5];

            // Access the ArtifactCollection of the page
            ArtifactCollection artifacts = page.Artifacts;

            // First watermark artifact
            var wm1 = new WatermarkArtifact
            {
                Text = "Confidential",
                // Text styling lives in TextState
                TextState = new TextState
                {
                    FontSize = 72,
                    ForegroundColor = Color.FromRgb(1, 0, 0), // Red
                    Font = FontRepository.FindFont("Arial")
                },
                Opacity = 0.5,
                Position = new Point(100, 500) // X, Y coordinates (Aspose.Pdf.Point)
            };
            artifacts.Add(wm1);

            // Second watermark artifact
            var wm2 = new WatermarkArtifact
            {
                Text = "Draft",
                TextState = new TextState
                {
                    FontSize = 48,
                    ForegroundColor = Color.FromRgb(0, 0, 1), // Blue
                    Font = FontRepository.FindFont("Arial")
                },
                Opacity = 0.3,
                Position = new Point(200, 300)
            };
            artifacts.Add(wm2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
