using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputImgPath = "firstImage.jpg";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = pdfDoc.Pages[1];

            bool imageSaved = false;

            // Iterate over images on the page; XImageCollection is not a dictionary
            foreach (XImage img in firstPage.Resources.Images)
            {
                // Save the first encountered image as JPEG.
                // XImage.Save expects a Stream and a System.Drawing.Imaging.ImageFormat.
                using (FileStream fs = new FileStream(outputImgPath, FileMode.Create, FileAccess.Write))
                {
                    img.Save(fs, ImageFormat.Jpeg);
                }
                imageSaved = true;
                break; // only the first image is required
            }

            if (imageSaved)
                Console.WriteLine($"First image extracted and saved to '{outputImgPath}'.");
            else
                Console.WriteLine("No images found on the first page.");
        }
    }
}
