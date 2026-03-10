using System;
using System.IO;
using Aspose.Pdf;

class ExtractImages
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Each page has a collection of XImage objects (not a dictionary)
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a file name – Aspose.Pdf preserves the original image format
                    // when XImage.Save is called, regardless of the file extension.
                    string extension = Path.GetExtension(xImg.Name);
                    if (string.IsNullOrEmpty(extension))
                        extension = ".img"; // fallback if the image name has no extension

                    string outPath = Path.Combine(outputFolder,
                        $"Page{pageNum}_Image{imageIndex}{extension}");

                    // Save the image to disk; XImage.Save expects a Stream in recent versions,
                    // so we open a FileStream and pass it.
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(fs);
                    }

                    Console.WriteLine($"Saved image {imageIndex} from page {pageNum} to '{outPath}'");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
