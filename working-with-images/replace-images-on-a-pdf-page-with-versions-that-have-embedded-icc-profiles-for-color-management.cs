using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string iccImagePath = "image_with_icc.jpg"; // JPEG that already contains an ICC profile

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(iccImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {iccImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // XImageCollection holds the images used on the page
                var images = page.Resources.Images;

                // Replace each image with the ICC‑profile version
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Open the replacement image as a stream
                    using (FileStream imgStream = File.OpenRead(iccImagePath))
                    {
                        // Replace image at the given 1‑based index
                        images.Replace(imgIndex, imgStream);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with ICC‑profile images saved to '{outputPdf}'.");
    }
}