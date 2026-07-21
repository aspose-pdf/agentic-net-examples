using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Image devices (BmpDevice, Resolution, etc.)

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";          // Source XML file
        const string outputDir = "PageImages";         // Folder for exported images

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a PDF document from the XML content
        using (Document pdfDoc = new Document())
        {
            // Bind the XML to the document – this generates the PDF structure
            pdfDoc.BindXml(xmlPath);

            // Configure image resolution (e.g., 300 DPI) for the exported images
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Export each page as a separate BMP image file
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++) // 1‑based indexing
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNum}.bmp");
                using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                {
                    // Convert the specific page to BMP and write to the stream
                    bmpDevice.Process(pdfDoc.Pages[pageNum], outStream);
                }
                Console.WriteLine($"Saved page {pageNum} → {outPath}");
            }
        }
    }
}