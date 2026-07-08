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

        // Load the XML file and convert it to a PDF document
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Verify that the PDF contains at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The generated PDF has no pages.");
                return;
            }

            // ThumbnailDevice does NOT implement IDisposable, so instantiate it without a using block
            ThumbnailDevice thumbDevice = new ThumbnailDevice(200, 200);

            // Retrieve the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = pdfDoc.Pages[1];

            // Write the thumbnail image to a file (dispose only the stream)
            using (FileStream outStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
            {
                thumbDevice.Process(firstPage, outStream);
            }
        }

        Console.WriteLine($"Thumbnail saved to '{thumbnailPath}'.");
    }
}
