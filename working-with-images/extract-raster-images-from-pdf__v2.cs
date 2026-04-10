using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Tagged;             // For ITaggedContent (not needed here but kept for consistency)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF containing raster images
        const string outputDir = "ExtractedImages";    // Folder where images will be saved

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            int globalImgCounter = 1; // Counter to generate unique file names when needed

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all XImage objects on the current page.
                // XImageCollection is not a dictionary; direct foreach is required.
                foreach (XImage img in page.Resources.Images)
                {
                    // Determine a file name.
                    // XImage.Name returns the original resource name (may include an extension).
                    // If the name is missing, create a unique one.
                    string fileName = img.Name;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = $"image_{globalImgCounter}";
                    }

                    // Ensure the file name has an extension.
                    // XImage.Save preserves the original image format regardless of the extension,
                    // but providing a proper extension makes the saved file easier to identify.
                    string extension = Path.GetExtension(fileName);
                    if (string.IsNullOrEmpty(extension))
                    {
                        // Default to .bin when the original format is unknown.
                        extension = ".bin";
                        fileName += extension;
                    }

                    string outputPath = Path.Combine(outputDir, fileName);

                    // XImage.Save expects a Stream, not a file path. Use a FileStream to write the image.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outputPath}");
                    globalImgCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
