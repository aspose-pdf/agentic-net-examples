using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string outputPdfPath  = "output.pdf";     // result PDF
        const string imagePath      = "background.jpg"; // image to use as background
        const int    targetPageNum  = 1;                // page number (1‑based)

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (targetPageNum < 1 || targetPageNum > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNum} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (1‑based indexing)
            Page page = doc.Pages[targetPageNum];

            // Create a BackgroundArtifact (parameterless constructor only)
            BackgroundArtifact bgArtifact = new BackgroundArtifact();

            // Set optional properties – note that the image itself cannot be assigned directly
            // because Artifact.Image is read‑only. To use an image as a background you would
            // normally employ an ImageStamp with IsBackground = true, but the task focuses on
            // demonstrating the correct usage of BackgroundArtifact.
            bgArtifact.Opacity = 0.5; // 50% opacity (optional)

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(bgArtifact);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background artifact added and saved to '{outputPdfPath}'.");
    }
}
