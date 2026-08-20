using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Paths for the source XML and the output PDF and images
        const string xmlPath = "source.xml";
        const string pdfPath = "output.pdf";
        const string imagesFolder = "PageImages";

        // Verify source file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        // Load the XML content and convert it to a PDF document
        using (Document pdfDoc = new Document())
        {
            // Bind the XML to the document (no special load options required)
            pdfDoc.BindXml(xmlPath);

            // Save the intermediate PDF (optional, but useful for verification)
            pdfDoc.Save(pdfPath);

            // Define the desired image resolution (dots per inch)
            const int dpi = 300;
            Resolution imageResolution = new Resolution(dpi);

            // Create a PNG device with the specified resolution.
            // PNG is chosen for loss‑less output; you can use JpegDevice, BmpDevice, etc.
            PngDevice pngDevice = new PngDevice(imageResolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(imagesFolder, $"Page_{pageNumber}.png");

                // Export the current page to a PNG image using the defined resolution
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Exported page {pageNumber} to '{imagePath}' at {dpi} DPI.");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
