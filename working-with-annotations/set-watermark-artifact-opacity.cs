using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Enumerate artifacts on the current page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify watermark artifacts by their subtype
                    if (artifact.Subtype == Artifact.ArtifactSubtype.Watermark)
                    {
                        // Set opacity to 50 % (range 0..1)
                        artifact.Opacity = 0.5;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All watermark artifacts updated to 50% opacity and saved as '{outputPath}'.");
    }
}