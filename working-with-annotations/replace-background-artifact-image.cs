using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output.pdf";
        const string highResImagePath  = "highres.png";

        if (!File.Exists(inputPdfPath) || !File.Exists(highResImagePath))
        {
            Console.Error.WriteLine("Input PDF or high‑resolution image not found.");
            return;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all artifacts on the page (1‑based indexing)
                for (int i = 1; i <= page.Artifacts.Count; i++)
                {
                    Artifact artifact = page.Artifacts[i];

                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image with a higher‑resolution version
                        // SetImage reads the stream immediately, so the stream can be disposed after the call
                        using (FileStream imgStream = File.OpenRead(highResImagePath))
                        {
                            bgArtifact.SetImage(imgStream);
                        }
                    }
                }
            }

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated background image: {outputPdfPath}");
    }
}