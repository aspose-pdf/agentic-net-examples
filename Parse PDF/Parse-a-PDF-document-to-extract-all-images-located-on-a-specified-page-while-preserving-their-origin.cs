using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Page number to extract images from (1‑based indexing)
        const int pageNumber = 1;

        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Validate the requested page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                // Access the image resources of the specified page
                var images = doc.Pages[pageNumber].Resources.Images;

                int imageIndex = 1;
                foreach (XImage img in images)
                {
                    // Build a file name for the extracted image.
                    // The extension ".img" is used as a placeholder; the original format is preserved by Aspose.
                    string outPath = Path.Combine(outputDir,
                        $"page{pageNumber}_image{imageIndex}.img");

                    // Save the image using a FileStream because XImage.Save expects a Stream.
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image {imageIndex} to {outPath}");
                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images found on the specified page.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
