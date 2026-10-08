using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string basePdfPath   = "base.pdf";          // initial PDF (will be created)
        const string outputPdfPath = "output_with_images.pdf";
        string[] imageFiles = { "image1.jpg", "image2.png", "image3.tif" };

        // Ensure the base PDF exists – create a simple one‑page PDF if needed
        if (!File.Exists(basePdfPath))
        {
            using (Document doc = new Document())
            {
                // Add a blank page so that images have a page to be placed on
                doc.Pages.Add();
                doc.Save(basePdfPath);
            }
        }

        // Copy the base PDF to the output path so that we work on a fresh file
        File.Copy(basePdfPath, outputPdfPath, true);

        // Load the PDF that we will modify
        using (Document pdfDoc = new Document(outputPdfPath))
        {
            // We will add all images to the first page (1‑based index)
            const int pageNumber = 1;
            Page page = pdfDoc.Pages[pageNumber];

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image not found: {imgPath}");
                    continue;
                }

                try
                {
                    // Create an Image object and bind the file stream
                    var img = new Aspose.Pdf.Image
                    {
                        ImageStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read)
                    };

                    // Optional: set a rectangle to control position/size. Here we use the example coordinates.
                    // The rectangle is defined as llx, lly, urx, ury (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y).
                    img.FixWidth = 500;   // width = urx - llx (example)
                    img.FixHeight = 250;  // height = ury - lly (example)
                    img.Margin = new MarginInfo { Left = 50, Top = 500 }; // example placement

                    // Add the image to the page's paragraphs collection
                    page.Paragraphs.Add(img);
                }
                catch (Exception ex)
                {
                    // Log the problematic image file path and the exception message
                    Console.Error.WriteLine($"Failed to add image '{imgPath}': {ex.Message}");
                }
            }

            // Save the modified PDF back to the same file
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image insertion completed. Result saved to '{outputPdfPath}'.");
    }
}
