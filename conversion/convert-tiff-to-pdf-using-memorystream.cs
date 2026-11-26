using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TIFF image
        const string tiffPath   = "source.tif";
        // Path for the resulting PDF
        const string pdfPath    = "output.pdf";

        // Verify the TIFF file exists
        if (!File.Exists(tiffPath))
        {
            Console.Error.WriteLine($"TIFF file not found: {tiffPath}");
            return;
        }

        // Read the TIFF bytes and wrap them in a MemoryStream
        byte[] tiffBytes = File.ReadAllBytes(tiffPath);
        using (MemoryStream tiffStream = new MemoryStream(tiffBytes))
        {
            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a blank page to the document
                Page page = pdfDoc.Pages.Add();

                // Create an Image object and assign the MemoryStream as its source
                Image pdfImage = new Image
                {
                    ImageStream = tiffStream
                };

                // Add the image to the page's paragraph collection
                page.Paragraphs.Add(pdfImage);

                // Save the PDF to disk
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"TIFF converted to PDF successfully: {pdfPath}");
    }
}