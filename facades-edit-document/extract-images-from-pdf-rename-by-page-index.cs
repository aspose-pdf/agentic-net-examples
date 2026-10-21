using System;
using System.IO;
using Aspose.Pdf;

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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page (Aspose.Pdf pages are 1‑based)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                var page = doc.Pages[pageNumber];
                int imageIndex = 1;

                // Extract images from the page's resources
                foreach (var image in page.Resources.Images)
                {
                    // Build the output file name: Image_Page{page}_Index{index}.png
                    string fileName = $"Image_Page{pageNumber}_Index{imageIndex}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the image to disk (Aspose.Pdf.Image implements Save(Stream))
                    using (FileStream outFile = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        image.Save(outFile);
                    }

                    Console.WriteLine($"Saved: {outPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
