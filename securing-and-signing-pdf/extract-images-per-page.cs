using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";          // Path to the signed PDF
        const string outputRoot = "ExtractedImages";    // Root folder for all pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a folder for the current page
                string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                Directory.CreateDirectory(pageFolder);

                int imageIndex = 1;
                // Iterate over all images referenced by this page
                foreach (XImage img in page.Resources.Images)
                {
                    // Simple extension selection – PNG works for all image types
                    string filePath = Path.Combine(pageFolder, $"Image_{imageIndex}.png");

                    // Save the image using a FileStream (lifecycle: save)
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All images have been extracted to page‑specific folders.");
    }
}
