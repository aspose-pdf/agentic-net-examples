using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string highResImgPath = "highres.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResImgPath))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImgPath}");
            return;
        }

        // Load the PDF document (using the standard load constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through artifacts on the page (also 1‑based)
                for (int i = 1; i <= page.Artifacts.Count; i++)
                {
                    Artifact artifact = page.Artifacts[i];

                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image with the higher‑resolution version.
                        // SetImage copies the image data, so the source stream can be closed safely.
                        using (FileStream imgStream = File.OpenRead(highResImgPath))
                        {
                            bgArtifact.SetImage(imgStream);
                        }
                    }
                }
            }

            // Save the modified PDF (standard Save overload)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated background image: {outputPdfPath}");
    }
}