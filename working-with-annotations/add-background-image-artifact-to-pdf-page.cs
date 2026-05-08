using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string outputPdfPath  = "output.pdf";     // result PDF
        const string backgroundImg  = "background.png"; // image to use as background

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImg))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImg}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page to which the background artifact will be added (e.g., first page)
            Page page = doc.Pages[1]; // Pages are 1‑based

            // Create a BackgroundArtifact instance
            BackgroundArtifact bgArtifact = new BackgroundArtifact();

            // Assign the image to the artifact (file path overload)
            bgArtifact.SetImage(backgroundImg);

            // Ensure the artifact is placed behind page contents
            bgArtifact.IsBackground = true;

            // Optionally adjust opacity (0.0 = fully transparent, 1.0 = fully opaque)
            bgArtifact.Opacity = 0.5f;

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(bgArtifact);

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with background artifact: {outputPdfPath}");
    }
}