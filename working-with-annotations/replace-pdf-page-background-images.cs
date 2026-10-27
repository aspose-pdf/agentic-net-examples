using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp resides here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newImagePath = "newImage.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect background artifacts first because we will modify the collection while iterating
                List<BackgroundArtifact> bgArtifacts = new List<BackgroundArtifact>();
                foreach (Artifact art in page.Artifacts)
                {
                    if (art is BackgroundArtifact bg)
                        bgArtifacts.Add(bg);
                }

                foreach (BackgroundArtifact bg in bgArtifacts)
                {
                    // Preserve the original rectangle (position and size)
                    Aspose.Pdf.Rectangle rect = bg.Rectangle;

                    // Remove the original background artifact using Delete (no out parameter needed)
                    page.Artifacts.Delete(bg);

                    // Create an ImageStamp that occupies the same rectangle
                    ImageStamp stamp = new ImageStamp(newImagePath)
                    {
                        XIndent = rect.LLX,
                        YIndent = rect.LLY,
                        Width = rect.Width,
                        Height = rect.Height,
                        Background = true // place it on the page background
                    };

                    // Add the new image stamp to the page
                    page.AddStamp(stamp);
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background artifacts replaced and saved to '{outputPdf}'.");
    }
}
