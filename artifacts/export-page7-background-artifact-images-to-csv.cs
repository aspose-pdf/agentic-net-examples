using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // Artifact types are defined here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "page7_background_artifacts.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 7 is the seventh page
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document has fewer than 7 pages.");
                return;
            }

            Page page = doc.Pages[7];

            // Filter artifacts to only BackgroundArtifact instances
            var backgroundArtifacts = page.Artifacts
                                          .OfType<BackgroundArtifact>()
                                          .Where(a => a.Image != null);

            // Write the image names to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                writer.WriteLine("ImageFileName"); // CSV header

                foreach (BackgroundArtifact artifact in backgroundArtifacts)
                {
                    // XImage.Name holds the internal PDF image name (e.g., Im1, Im2)
                    string imageName = artifact.Image.Name ?? string.Empty;
                    writer.WriteLine(imageName);
                }
            }

            Console.WriteLine($"CSV file created: {outputCsvPath}");
        }
    }
}