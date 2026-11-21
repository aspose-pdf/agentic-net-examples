using System;
using System.IO;
using Aspose.Pdf;

class ReplaceLowResImages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string highResFolder = "HighResImages"; // folder containing high‑resolution PNGs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution image folder not found: {highResFolder}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over each image resource on the page using index‑based access
                for (int imgIndex = 1; imgIndex <= page.Resources.Images.Count; imgIndex++)
                {
                    XImage img = page.Resources.Images[imgIndex];

                    // Obtain a name for the image; fall back to a generated name if missing
                    string imgName = img.Name;
                    if (string.IsNullOrEmpty(imgName))
                        imgName = $"Image_{imgIndex}";

                    // Build the expected high‑resolution PNG file path
                    string highResPath = Path.Combine(highResFolder, imgName + ".png");

                    // If a matching high‑resolution PNG exists, replace the image data
                    if (File.Exists(highResPath))
                    {
                        using (FileStream highResStream = File.OpenRead(highResPath))
                        {
                            // Replace the image at the current index with the new stream
                            page.Resources.Images.Replace(imgIndex, highResStream);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Low‑resolution images replaced. Output saved to '{outputPdfPath}'.");
    }
}
