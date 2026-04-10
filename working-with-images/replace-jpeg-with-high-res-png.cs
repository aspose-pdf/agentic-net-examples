using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // result PDF
        const string newPng = "high_res_image.png"; // replacement image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(newPng))
        {
            Console.Error.WriteLine($"Replacement image not found: {newPng}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain a third page.");
                return;
            }

            // Access page three (1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[3];

            // Access the image collection of the page
            Aspose.Pdf.XImageCollection images = page.Resources.Images;

            // If there is at least one image, replace the first one
            if (images.Count > 0)
            {
                // Open the PNG file as a stream
                using (FileStream pngStream = File.OpenRead(newPng))
                {
                    // Replace image at index 1 (XImageCollection is 1‑based)
                    images.Replace(1, pngStream);
                }
            }
            else
            {
                Console.WriteLine("No images found on page 3 to replace.");
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with replaced image: {outputPdf}");
    }
}
