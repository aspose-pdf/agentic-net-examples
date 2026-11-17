using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output.pdf";         // result PDF
        const string newImagePath  = "newImage.jpg";       // image to replace backgrounds

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (pages are 1‑based, but foreach abstracts that)
            foreach (Page page in doc.Pages)
            {
                // Iterate over the artifacts collection of the page
                // Use a for‑loop because we may need the index for replacement
                for (int i = 0; i < page.Artifacts.Count; i++)
                {
                    Artifact artifact = page.Artifacts[i];

                    // Identify BackgroundArtifact instances
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Preserve the original position (the Position property is a Rectangle)
                        // No need to change it – we only replace the image.

                        // Replace the background image. BackgroundImage is writable, unlike Artifact.Image.
                        // The stream is opened just for the assignment; Aspose reads the data immediately.
                        using (FileStream fs = File.OpenRead(newImagePath))
                        {
                            bgArtifact.BackgroundImage = fs;
                        }
                    }
                }
            }

            // Save the modified document (save inside the using block so the Document stays alive)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background artifacts replaced and saved to '{outputPdfPath}'.");
    }
}
