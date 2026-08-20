using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the source XML file that will be transformed into a PDF.
        const string xmlPath = "input.xml";

        // Directory where each page image will be saved.
        const string outputDir = "PageImages";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the XML and create a PDF document from it.
        using (Document pdfDoc = new Document())
        {
            // Bind the XML content to the document.
            pdfDoc.BindXml(xmlPath);

            // Optional: save the generated PDF if you need it.
            // pdfDoc.Save(Path.Combine(outputDir, "generated.pdf"));

            // Prepare an image device – BMP format with 300 DPI resolution.
            Resolution resolution = new Resolution(300);
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Export each page as a separate BMP image file.
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string imagePath = Path.Combine(outputDir, $"page_{pageNum}.bmp");
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Create))
                {
                    // Convert the current page to an image and write it to the stream.
                    bmpDevice.Process(pdfDoc.Pages[pageNum], imgStream);
                }
                Console.WriteLine($"Saved page {pageNum} as {imagePath}");
            }
        }
    }
}