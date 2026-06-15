using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // BmpDevice and Resolution

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";          // source XML file
        const string outputDir = "ExportedImages";     // folder for images

        // Verify XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create PDF from XML
        using (Document pdfDoc = new Document())
        {
            // Bind the XML to the document – this generates the PDF content
            pdfDoc.BindXml(xmlPath);

            // Optional: define image resolution (300 DPI is a good default)
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Export each page as a separate BMP image
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                bmpDevice.Process(pdfDoc.Pages[pageNumber], imagePath);
                Console.WriteLine($"Page {pageNumber} saved as {imagePath}");
            }
        }
    }
}