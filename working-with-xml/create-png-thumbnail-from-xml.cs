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

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        // XmlLoadOptions is required for XML → PDF conversion
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The generated PDF has no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = pdfDoc.Pages[1];

            // ThumbnailDevice does NOT implement IDisposable, so instantiate without a using block
            ThumbnailDevice thumbDevice = new ThumbnailDevice();

            // Save the thumbnail image to a file stream (stream is disposable)
            using (FileStream outStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the page to a PNG thumbnail and write it to the stream
                thumbDevice.Process(firstPage, outStream);
            }
        }

        Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
    }
}
