using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the signed PDF file
        const string inputPdf = "signed_input.pdf";

        // Root folder where page‑specific subfolders will be created
        const string outputRoot = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Create a folder for the current page (e.g., ExtractedImages/Page_1)
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageNum}");
                Directory.CreateDirectory(pageFolder);

                int imageIndex = 1;

                // Iterate over images; XImageCollection is not a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    // Save each image as PNG (you can change the extension if desired)
                    string imagePath = Path.Combine(pageFolder, $"Image_{imageIndex}.png");
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs); // XImage.Save expects a Stream, not a file path string
                    }
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
