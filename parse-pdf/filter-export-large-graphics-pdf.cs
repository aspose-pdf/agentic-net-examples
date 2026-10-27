using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";
        // Minimum area (width * height) in points^2 to keep an image
        const double minArea = 5000.0;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                int imageCounter = 0;

                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate over the image resources of the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Width and Height are in points; compute the area
                        double width = img.Width;
                        double height = img.Height;
                        double area = width * height;

                        // Export only images whose area exceeds the defined threshold
                        if (area >= minArea)
                        {
                            string outPath = Path.Combine(outputDir, $"image_{++imageCounter}.png");
                            // XImage.Save expects a Stream, not a file path string
                            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs);
                            }
                            Console.WriteLine($"Saved image #{imageCounter} (area={area}) to {outPath}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
