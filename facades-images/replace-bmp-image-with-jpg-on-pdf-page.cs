using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string oldBmp    = "image_to_replace.bmp";
        const string newJpg    = "new_image.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(oldBmp))
        {
            Console.Error.WriteLine($"BMP image not found: {oldBmp}");
            return;
        }
        if (!File.Exists(newJpg))
        {
            Console.Error.WriteLine($"JPG image not found: {newJpg}");
            return;
        }

        // Load the PDF using the high‑level Document API (available in all recent Aspose.Pdf versions)
        Document pdf = new Document(inputPdf);

        int pageNumber = 4; // pages are 1‑based
        int imageIndex = -1;

        // Load the BMP we want to replace so we can compare raw bytes
        byte[] oldBmpBytes = File.ReadAllBytes(oldBmp);

        // Iterate through the XImage collection on the target page.
        // XImageCollection is 1‑based; we keep a manual counter.
        int currentIdx = 1;
        foreach (XImage xImg in pdf.Pages[pageNumber].Resources.Images)
        {
            // Save the embedded image to a memory stream and compare its bytes with the BMP file.
            using (var ms = new MemoryStream())
            {
                xImg.Save(ms);
                if (ms.Length == oldBmpBytes.Length && ms.ToArray().SequenceEqual(oldBmpBytes))
                {
                    imageIndex = currentIdx;
                    break;
                }
            }
            currentIdx++;
        }

        if (imageIndex == -1)
        {
            Console.Error.WriteLine($"BMP image '{oldBmp}' not found on page {pageNumber}.");
            return;
        }

        // Replace the found image with the new JPG while keeping the original rectangle unchanged.
        // The Replace method works on the XImageCollection and keeps the image's position.
        using (FileStream newImgStream = File.OpenRead(newJpg))
        {
            pdf.Pages[pageNumber].Resources.Images.Replace(imageIndex, newImgStream);
        }

        // Save the modified PDF
        pdf.Save(outputPdf);

        Console.WriteLine($"Image replaced and PDF saved to '{outputPdf}'.");
    }
}
