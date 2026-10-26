using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image class lives here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string bannerImagePath = "banner_high_res.jpg";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(bannerImagePath))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Title page is the first page (1‑based indexing)
            Page titlePage = doc.Pages[1];

            // Remove all existing images from the page resources
            titlePage.Resources.Images.Clear();

            // Load the banner image via ImageStream (Aspose.Pdf.Image has no path ctor)
            using (FileStream bannerStream = new FileStream(bannerImagePath, FileMode.Open, FileAccess.Read))
            {
                Image banner = new Image
                {
                    ImageStream = bannerStream,
                    // Scale the banner to fill the entire page while preserving layout
                    FixWidth = titlePage.PageInfo.Width,
                    FixHeight = titlePage.PageInfo.Height
                };

                // Insert the banner at the beginning of the page content
                titlePage.Paragraphs.Insert(0, banner);

                // Save the modified PDF while the stream is still open
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Banner replaced on title page. Output saved to '{outputPdfPath}'.");
    }
}
