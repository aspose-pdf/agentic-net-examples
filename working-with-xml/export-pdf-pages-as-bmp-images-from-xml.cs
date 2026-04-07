using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Image devices and Resolution

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

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load XML and create a PDF document from it
        using (Document pdfDoc = new Document())
        {
            // Correct way to load XML into a PDF document
            pdfDoc.BindXml(xmlPath);
            pdfDoc.Info.Title = Path.GetFileNameWithoutExtension(xmlPath);

            // Set up an image device (BMP) with desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Export each page as a separate image file
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string imagePath = Path.Combine(outputDir, $"Page_{pageNum}.bmp");
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Create))
                {
                    // Convert the specific page to an image and write to the stream
                    bmpDevice.Process(pdfDoc.Pages[pageNum], imgStream);
                }
                Console.WriteLine($"Saved page {pageNum} as image: {imagePath}");
            }
        }
    }
}