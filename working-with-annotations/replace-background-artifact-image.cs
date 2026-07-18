using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string highResImgPath = "highres.jpg";

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

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing internally,
            // but foreach abstracts that detail)
            foreach (Page page in doc.Pages)
            {
                // Examine each artifact on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image with a higher‑resolution version.
                        // SetImage(string) loads the image from the file path and keeps the layout unchanged.
                        bgArtifact.SetImage(highResImgPath);
                    }
                }
            }

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with updated background image: '{outputPdfPath}'.");
    }
}