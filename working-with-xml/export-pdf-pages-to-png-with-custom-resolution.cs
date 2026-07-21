using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Output folder for exported images
        const string outFolder = "ExportedImages";
        Directory.CreateDirectory(outFolder);

        // Minimal XML that defines a PDF with a single empty page.
        string xmlContent = @"<?xml version='1.0' encoding='utf-8'?>
<Document>
    <Pages>
        <Page></Page>
    </Pages>
</Document>";

        // Create a PDF document and bind the XML string via a memory stream
        using (Document pdfDoc = new Document())
        {
            using (MemoryStream xmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlContent)))
            {
                pdfDoc.BindXml(xmlStream);
            }

            // Define the desired image resolution (e.g., 300 DPI)
            Resolution imageResolution = new Resolution(300);

            // Create an image device (PNG) with the specified resolution
            PngDevice pngDevice = new PngDevice(imageResolution);

            // Export each PDF page as a separate PNG image
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(outFolder, $"Page_{pageNumber}.png");
                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }
            }
        }

        Console.WriteLine("Image export completed.");
    }
}
