using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all images on the page to detect large ones (optional logging)
                foreach (XImage img in page.Resources.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);
                        long sizeInBytes = ms.Length;
                        if (sizeInBytes > 1_048_576) // 1 MB
                        {
                            // Log the large image – actual compression will be done globally later
                            Console.WriteLine($"Found large image ({sizeInBytes / 1024} KB) on page {i} – will be compressed.");
                        }
                    }
                }
            }

            // Apply global optimization that recompresses images (including those >1 MB)
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressImages = true;          // enable image recompression
            opt.ImageQuality = 50;              // adjust quality (0‑100) as needed
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            doc.OptimizeResources(opt);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
