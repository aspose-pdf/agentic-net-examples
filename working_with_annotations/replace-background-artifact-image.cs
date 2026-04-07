using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string highResImagePath = "highres_image.jpg";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image with the higher‑resolution version
                        using (FileStream imgStream = File.OpenRead(highResImagePath))
                        {
                            // SetImage replaces the existing background image while preserving layout
                            bgArtifact.SetImage(imgStream);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated background image: {outputPdfPath}");
    }
}