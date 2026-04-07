using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Image devices (PngDevice, Resolution, etc.)

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";               // Source XML file
        const string pdfPath = "output.pdf";               // Intermediate PDF
        const string imagePattern = "page_{0}.png";        // Output image files
        const int dpi = 300;                               // Desired image resolution

        // Verify source file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }

        // Load XML, convert to PDF, then export each page as an image with the specified DPI
        using (Document doc = new Document())
        {
            // Load XML content into the document
            doc.BindXml(xmlPath);

            // Save the intermediate PDF (optional, but demonstrates the conversion)
            doc.Save(pdfPath);

            // Create a Resolution object with the desired DPI
            Resolution resolution = new Resolution(dpi);

            // Initialize an image device (PNG) with the resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Export each PDF page to a PNG image
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                string imagePath = string.Format(imagePattern, pageNumber);
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    pngDevice.Process(doc.Pages[pageNumber], imageStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as image: {imagePath}");
            }
        }
    }
}