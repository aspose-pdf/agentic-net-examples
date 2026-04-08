using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Folder where extracted PNG images will be saved
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            int imageIndex = 0;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over the image resources of the page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    string outPath = Path.Combine(
                        outputFolder,
                        $"image_page{pageNum}_{imageIndex}.png");

                    // Save each embedded image as PNG using the Stream overload.
                    // The overload that accepts a string is not available for XImage, so we use a FileStream.
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine("All embedded images have been extracted as PNG files.");
    }
}
