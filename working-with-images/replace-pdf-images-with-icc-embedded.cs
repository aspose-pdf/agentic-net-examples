using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithICC
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string iccImagePath   = "image_with_icc.jpg"; // JPEG that already contains an ICC profile
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(iccImagePath))
        {
            Console.Error.WriteLine($"ICC‑embedded image not found: {iccImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Access the collection of images on the current page
                XImageCollection images = page.Resources.Images;

                // Replace each image with the ICC‑profile‑embedded version
                // XImageCollection indexing is also 1‑based
                for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                {
                    // Open a stream for the replacement image
                    using (FileStream replacementStream = File.OpenRead(iccImagePath))
                    {
                        // Replace the image at the given index
                        images.Replace(imgIdx, replacementStream);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdfPath}'.");
    }
}