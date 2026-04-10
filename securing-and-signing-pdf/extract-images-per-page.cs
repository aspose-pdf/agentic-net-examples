using System;
using System.IO;
using Aspose.Pdf;

class ExtractImagesPerPage
{
    static void Main()
    {
        const string inputPdfPath = "signed_input.pdf";
        const string outputRoot   = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (signed PDFs can be opened without a password if not encrypted)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Create a folder for the current page
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageNum}");
                Directory.CreateDirectory(pageFolder);

                int imageIndex = 1;

                // Iterate over images on the page (XImageCollection is not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a file name for the extracted image
                    string imagePath = Path.Combine(pageFolder, $"Image_{imageIndex}.png");

                    // Save the image to the file system using a stream overload (required by newer Aspose.Pdf versions)
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved page {pageNum} image {imageIndex} to '{imagePath}'");
                    imageIndex++;
                }

                // If a page has no images, inform the user
                if (imageIndex == 1)
                {
                    Console.WriteLine($"Page {pageNum} contains no images.");
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
