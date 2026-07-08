using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string newImage  = "newBackground.png"; // image to use for all backgrounds

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(newImage))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Load the PDF (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Process only background artifacts
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image while preserving all other properties
                        // (Position, Rotation, Opacity, alignment, etc. remain unchanged)
                        bgArtifact.SetImage(newImage);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with new background images to '{outputPdf}'.");
    }
}