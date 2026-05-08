using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string bannerImg = "banner.jpg";  // new branding image

        if (!File.Exists(inputPdf) || !File.Exists(bannerImg))
        {
            Console.Error.WriteLine("Input PDF or banner image not found.");
            return;
        }

        // Load the PDF (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Assume the index page is the first page (1‑based indexing)
            Page indexPage = doc.Pages[1];

            // If the page already contains images, replace the first one
            if (indexPage.Resources.Images.Count > 0)
            {
                using (FileStream bannerStream = File.OpenRead(bannerImg))
                {
                    // XImageCollection.Replace uses 1‑based index
                    indexPage.Resources.Images.Replace(1, bannerStream);
                }
            }
            else
            {
                // No existing images – add the banner as a new image
                using (FileStream bannerStream = File.OpenRead(bannerImg))
                {
                    // Define where the banner should appear (full width at top)
                    double pageWidth  = indexPage.PageInfo.Width;
                    double pageHeight = indexPage.PageInfo.Height;
                    Aspose.Pdf.Rectangle bannerRect = new Aspose.Pdf.Rectangle(
                        0,                     // left
                        pageHeight - 100,      // bottom (100 pts height banner)
                        pageWidth,             // right
                        pageHeight);           // top

                    // Add the image to the page at the specified rectangle
                    indexPage.AddImage(bannerStream, bannerRect);
                }
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated and saved to '{outputPdf}'.");
    }
}