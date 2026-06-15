using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
// No additional namespaces are required for this task.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path
        const string outputPdf = "output.pdf";
        // Image file to be used as background
        const string backgroundImagePath = "background.png";
        // Page number (1‑based) where the background will be applied
        const int targetPageNumber = 1;

        // Verify that the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Load the PDF document (using the mandatory load rule)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Create a BackgroundArtifact instance
            BackgroundArtifact bgArtifact = new BackgroundArtifact();

            // Set the image for the artifact.
            // The SetImage(string) overload accepts a file path.
            bgArtifact.SetImage(backgroundImagePath);

            // Mark the artifact as a background so it appears behind page contents
            bgArtifact.IsBackground = true;

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(bgArtifact);

            // Save the modified document (using the mandatory save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background artifact added and saved to '{outputPdf}'.");
    }
}