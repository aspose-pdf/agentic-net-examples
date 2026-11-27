using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Alias the conflicting System.IO.Path and System.Drawing.Imaging.ImageFormat types
using IOPath = System.IO.Path;
using ImgFormat = System.Drawing.Imaging.ImageFormat;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                int imageCounter = 1;

                // Iterate over the image collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a filename that includes the page number and image index
                    string fileName = $"Image_Page{pageNumber}_Img{imageCounter}.png";
                    string outputPath = IOPath.Combine(outputDir, fileName);

                    // Save the image to disk – XImage.Save expects a Stream, not a file path
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImgFormat.Png);
                    }

                    Console.WriteLine($"Saved: {outputPath}");
                    imageCounter++;
                }
            }
        }
    }
}
