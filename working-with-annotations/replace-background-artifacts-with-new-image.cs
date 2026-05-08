using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string newImagePath   = "newBackground.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over artifacts on the current page
                // ArtifactCollection also uses 1‑based indexing
                for (int i = 1; i <= page.Artifacts.Count; i++)
                {
                    Artifact artifact = page.Artifacts[i];

                    // Process only BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Position is already stored in bgArtifact.Position;
                        // we keep it unchanged and simply replace the image.
                        bgArtifact.SetImage(newImagePath);

                        // Ensure the artifact remains a background element
                        bgArtifact.IsBackground = true;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated background images: {outputPdfPath}");
    }
}