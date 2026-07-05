using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // Image devices (BmpDevice, etc.)

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";          // Source XML file
        const string outputDir = "PageImages";         // Folder for exported images

        // Verify XML input exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Create a PDF document from the XML using the BindXml API.
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlPath);   // Load XML and generate PDF structure

            // Set image resolution (e.g., 300 DPI) for better quality
            Resolution resolution = new Resolution(300);

            // Initialize a BMP image device with the chosen resolution
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // --------------------------------------------------------
            // Export each page of the generated PDF as a separate BMP.
            // --------------------------------------------------------
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.bmp");

                // Process the page and write the bitmap to a file stream
                using (FileStream bmpStream = new FileStream(outPath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDoc.Pages[pageNum], bmpStream);
                }

                Console.WriteLine($"Saved page {pageNum} → {outPath}");
            }
        }
    }
}