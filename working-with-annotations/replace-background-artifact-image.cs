using System;
using System.IO;
using Aspose.Pdf;

class ReplaceBackgroundArtifactImage
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string highResImagePath = "high_res_image.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(highResImagePath))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Each page may contain artifacts (e.g., background images)
                // Artifact collection is accessed via page.Artifacts
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the existing background image with a higher‑resolution one.
                        // BackgroundArtifact exposes a writable BackgroundImage property that accepts a Stream.
                        using (FileStream imgStream = File.OpenRead(highResImagePath))
                        {
                            bgArtifact.BackgroundImage = imgStream;
                        }
                        // No layout changes are required because the artifact keeps its
                        // original rectangle (position and size). Only the image data is swapped.
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background artifacts updated and saved to '{outputPdfPath}'.");
    }
}
