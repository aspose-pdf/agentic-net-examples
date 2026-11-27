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
        const string outputImagePath = "image2.jpg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Verify that the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Retrieve the second image on the page (index 1 in the collection)
            XImage secondImage = page.Resources.Images.ElementAtOrDefault(1);
            if (secondImage == null)
            {
                Console.Error.WriteLine("Second image not found on page 1.");
                return;
            }

            // Save the extracted image as JPEG – XImage.Save expects a Stream
            using (FileStream fs = File.Create(outputImagePath))
            {
                secondImage.Save(fs, ImageFormat.Jpeg);
            }

            Console.WriteLine($"Second image saved to '{outputImagePath}'.");
        }
    }
}
