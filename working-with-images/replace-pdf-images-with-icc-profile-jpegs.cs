using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithIcc
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string replacementImagePath = "replacement.jpg"; // JPEG with embedded ICC profile

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(replacementImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {replacementImagePath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing per rule: page-indexing-one-based)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                XImageCollection images = page.Resources.Images;

                // If the page contains images, replace each with the ICC‑profile‑embedded JPEG
                if (images.Count > 0)
                {
                    // Replace each image in the collection
                    for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                    {
                        // Open the replacement image stream
                        using (FileStream imgStream = File.OpenRead(replacementImagePath))
                        {
                            // Replace the image at the current index (XImageCollection.Replace expects 1‑based index)
                            images.Replace(imgIndex, imgStream);
                        }
                    }
                }
            }

            // Save the modified PDF (Document.Save(string) writes PDF regardless of extension)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with ICC‑profile images: {outputPdfPath}");
    }
}