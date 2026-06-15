using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string newImgPath = "newBackground.png";

        if (!File.Exists(inputPdf) || !File.Exists(newImgPath))
        {
            Console.Error.WriteLine("Input PDF or new image file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Check if the artifact is a background artifact
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image while keeping position and other properties unchanged
                        bgArtifact.SetImage(newImgPath);
                        // No need to modify Position; it remains the same
                    }
                }
            }

            // Save the modified document (lifecycle rule: using for disposal)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated background images to '{outputPdf}'.");
    }
}