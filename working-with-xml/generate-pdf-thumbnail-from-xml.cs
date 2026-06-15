using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string thumbnailPath = "thumb.png";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document in memory
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Verify that the PDF contains at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The generated PDF has no pages.");
                return;
            }

            // ThumbnailDevice does NOT implement IDisposable, so instantiate without a using block
            ThumbnailDevice thumbDevice = new ThumbnailDevice();

            // Generate a thumbnail of the first page and save it as PNG
            thumbDevice.Process(pdfDoc.Pages[1], thumbnailPath);
        }

        Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
    }
}
