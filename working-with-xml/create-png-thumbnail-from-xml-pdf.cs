using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Paths for input XML and output thumbnail image
        const string xmlPath = "input.xml";
        const string thumbnailPath = "thumbnail.png";

        // Ensure the input XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlPath}");
            return;
        }

        // Load the XML and convert it to a PDF document (in memory)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // ThumbnailDevice does NOT implement IDisposable – instantiate without a using block
            ThumbnailDevice thumbDevice = new ThumbnailDevice();

            // The first page in Aspose.Pdf is 1‑based
            Page firstPage = pdfDoc.Pages[1];

            // Write the PNG thumbnail to a file
            using (FileStream outStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
            {
                thumbDevice.Process(firstPage, outStream);
            }
        }

        Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
    }
}
