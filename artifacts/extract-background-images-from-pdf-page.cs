using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string outputDir = "BackgroundImages";   // folder for extracted images

        // Verify source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            int imageCounter = 1;

            // Iterate over all artifacts on the page
            foreach (Artifact artifact in page.Artifacts)
            {
                // Background artifacts are represented by the BackgroundArtifact class.
                // Use pattern matching to safely cast and check for an image.
                if (artifact is BackgroundArtifact bg && bg.Image != null)
                {
                    string outPath = Path.Combine(outputDir, $"bg_image_{imageCounter}.png");

                    // Save the XImage to disk. XImage.Save expects a Stream.
                    using (FileStream fs = File.Create(outPath))
                    {
                        bg.Image.Save(fs);
                    }

                    Console.WriteLine($"Saved background image {imageCounter} to '{outPath}'.");
                    imageCounter++;
                }
            }

            if (imageCounter == 1)
                Console.WriteLine("No background images found on page 3.");
        }
    }
}
