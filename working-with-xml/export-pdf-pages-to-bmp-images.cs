using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class ExportPdfPagesToImages
{
    static void Main()
    {
        // Paths
        const string xmlInputPath   = "input.xml";
        const string pdfOutputPath  = "generated.pdf";
        const string imagesFolder   = "PageImages";

        // Verify XML exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // 1. Create a PDF document from the XML source.
        // -----------------------------------------------------------------
        // XmlLoadOptions loads XML (XFA, XDP, etc.) into a PDF.
        // The class resides in the Aspose.Pdf namespace.
        using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Save the intermediate PDF (optional, useful for inspection)
            pdfDoc.Save(pdfOutputPath);

            // -----------------------------------------------------------------
            // 2. Export each page of the PDF as a separate PNG image.
            // -----------------------------------------------------------------
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Build the image file name, e.g., Page_1.png
                string imagePath = Path.Combine(imagesFolder, $"Page_{pageNumber}.png");

                // Use a PNG device to render the page.
                // The device writes the image directly to a stream.
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    // PngDevice is part of Aspose.Pdf.Devices and works cross‑platform.
                    // No additional options are required for a basic export.
                    PngDevice pngDevice = new PngDevice();
                    pngDevice.Process(page, imageStream);
                }

                Console.WriteLine($"Exported page {pageNumber} to '{imagePath}'.");
            }
        }

        Console.WriteLine("All pages have been exported as images.");
    }
}