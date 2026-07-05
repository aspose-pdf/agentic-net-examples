using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF (signed PDFs can be opened without a password if not encrypted)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                int imageIndex = 1;

                // Each page has a Resources.Images collection of XImage objects
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name per page and image
                    string outputPath = Path.Combine(
                        outputDir,
                        $"page{pageNumber}_img{imageIndex}.png");

                    // Save the image using a FileStream because XImage.Save expects a Stream
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All embedded images have been extracted.");
    }
}
