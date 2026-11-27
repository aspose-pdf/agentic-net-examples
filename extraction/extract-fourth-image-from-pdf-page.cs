using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputJpeg = "image4_page2.jpg";
        const int pageNumber = 2; // pages are 1‑based
        const int imageIndex = 4; // we want the 4th image on the page

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Locate the 4th image on the page
            XImage targetImage = null;
            int currentIndex = 0;
            foreach (XImage img in page.Resources.Images)
            {
                currentIndex++;
                if (currentIndex == imageIndex)
                {
                    targetImage = img;
                    break;
                }
            }

            if (targetImage == null)
            {
                Console.Error.WriteLine($"Image #{imageIndex} not found on page {pageNumber}.");
                return;
            }

            // Save the extracted image as JPEG using System.Drawing.Imaging.ImageFormat
            using (FileStream fs = new FileStream(outputJpeg, FileMode.Create, FileAccess.Write))
            {
                targetImage.Save(fs, ImageFormat.Jpeg);
            }

            Console.WriteLine($"Extracted image saved to '{outputJpeg}'.");
        }
    }
}
