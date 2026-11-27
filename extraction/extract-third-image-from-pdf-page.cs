using System;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputJpg = "extracted_image3.jpg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure page 2 exists (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            Page pageTwo = doc.Pages[2];

            // XImageCollection is enumerable, not a dictionary.
            // Use LINQ to get the third image (index 2) if it exists.
            XImage thirdImage = pageTwo.Resources.Images
                                 .Cast<XImage>()
                                 .ElementAtOrDefault(2); // zero‑based index

            if (thirdImage == null)
            {
                Console.Error.WriteLine("Page 2 does not contain at least three images.");
                return;
            }

            // Save the extracted image as JPEG using a FileStream.
            using (FileStream fs = new FileStream(outputJpg, FileMode.Create, FileAccess.Write))
            {
                thirdImage.Save(fs, ImageFormat.Jpeg);
            }

            Console.WriteLine($"Third image from page 2 saved as JPEG to '{outputJpg}'.");
        }
    }
}
