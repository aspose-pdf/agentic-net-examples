using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    // 1 MB threshold
    const long SizeThreshold = 1 * 1024 * 1024;

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            bool needsCompression = false;

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate the image collection directly (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    if (img == null)
                        continue;

                    // Determine the raw size of the image by saving it to a memory stream
                    long imageSize;
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms);
                        imageSize = ms.Length;
                    }

                    // If the image exceeds the 1 MB threshold, mark for compression
                    if (imageSize > SizeThreshold)
                    {
                        needsCompression = true;
                        break; // No need to continue checking other images on this page
                    }
                }

                if (needsCompression)
                    break; // Exit outer loop early – we will compress the whole document
            }

            // Apply compression if any large image was found
            if (needsCompression)
            {
                // Configure optimization options – compress images with JPEG quality 75
                OptimizationOptions opt = OptimizationOptions.All();
                opt.CompressImages = true;
                opt.ImageCompressionOptions.CompressImages = true;
                opt.ImageCompressionOptions.ImageQuality = 75; // JPEG quality (0‑100)
                opt.ImageCompressionOptions.ResizeImages = true;
                opt.ImageCompressionOptions.MaxResolution = 150; // optional DPI limit
                opt.RemoveUnusedObjects = true;
                opt.RemoveUnusedStreams = true;
                opt.LinkDuplicateStreams = true;
                opt.SubsetFonts = true;

                doc.OptimizeResources(opt);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Report size reduction
        long originalSize = new FileInfo(inputPath).Length;
        long newSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Original size: {originalSize:N0} bytes");
        Console.WriteLine($"Compressed size: {newSize:N0} bytes");
        Console.WriteLine($"Reduction: {originalSize - newSize:N0} bytes");
    }
}
