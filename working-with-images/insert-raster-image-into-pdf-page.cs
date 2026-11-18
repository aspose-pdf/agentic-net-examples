using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "picture.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Select the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Open the raster image as a stream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Use ImageStamp for absolute positioning on the page
                ImageStamp stamp = new ImageStamp(imgStream)
                {
                    // Absolute coordinates measured from the lower‑left corner of the page
                    XIndent = 100,   // X coordinate from the left edge
                    YIndent = 500,   // Y coordinate from the bottom edge
                    Width   = 200,   // Desired width of the image
                    Height  = 150    // Desired height of the image
                };

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}
