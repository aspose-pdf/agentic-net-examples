using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document (signed PDFs are handled automatically)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imgIndex = 1;

                // Iterate over all images defined in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name per page and image
                    string fileName = $"page_{pageNum}_img_{imgIndex}.png";
                    string outPath  = Path.Combine(outputDir, fileName);

                    // Save the image as PNG; Aspose.Pdf requires a Stream overload
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outPath}");
                    imgIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
