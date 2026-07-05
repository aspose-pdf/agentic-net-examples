using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_vectorized.pdf";
        const string vectorImagesFolder = "VectorImages"; // folder that holds .svg files

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all images on the current page
                for (int i = 1; i <= page.Resources.Images.Count; i++)
                {
                    // Retrieve the image object; XImage provides the internal name used in the PDF
                    XImage rasterImg = page.Resources.Images[i];
                    string imgName = rasterImg.Name; // e.g., "Image1"

                    // Build the expected path of the vector counterpart (e.g., "VectorImages/Image1.svg")
                    string vectorPath = Path.Combine(vectorImagesFolder, imgName + ".svg");

                    if (File.Exists(vectorPath))
                    {
                        // Replace the raster image with the vector image.
                        // XImageCollection.Replace expects a stream; Aspose.Pdf will handle the SVG format.
                        using (FileStream vecStream = File.OpenRead(vectorPath))
                        {
                            page.Resources.Images.Replace(i, vecStream);
                            Console.WriteLine($"Replaced raster image '{imgName}' with vector file '{vectorPath}'.");
                        }
                    }
                }
            }

            // Optional: clean up unused resources and compress the document.
            doc.OptimizeResources();

            // Save the updated PDF (lifecycle rule: use the Save method with a path)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vectorized PDF saved to '{outputPath}'.");
    }
}