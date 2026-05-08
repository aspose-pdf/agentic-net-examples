using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputDir = "PageImages";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Create an empty PDF document and bind the XML source to it.
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlPath); // Generates PDF content from the XML.

            // Device that renders a page to a BMP image (300 DPI resolution).
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Export each page as a separate image file.
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string imagePath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    bmpDevice.Process(pdfDoc.Pages[pageNum], stream);
                }
                Console.WriteLine($"Page {pageNum} saved to {imagePath}");
            }
        }
    }
}