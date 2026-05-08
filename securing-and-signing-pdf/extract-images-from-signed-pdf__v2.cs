using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf"; // path to the signed PDF
        const string outputRoot = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];

                // Create a folder for the current page
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageNumber}");
                Directory.CreateDirectory(pageFolder);

                int imageIndex = 1;
                // Iterate over all images in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    // Build the output file path (PNG format)
                    string imagePath = Path.Combine(pageFolder, $"Image_{imageIndex}.png");

                    // XImage.Save expects a Stream in this version of Aspose.Pdf.
                    // Open a FileStream and pass it to the Save method.
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved {imagePath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
