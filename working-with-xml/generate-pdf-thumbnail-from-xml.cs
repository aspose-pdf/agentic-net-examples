using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Paths for the source XML file, the intermediate PDF, and the thumbnail image.
        const string xmlPath = "input.xml";
        const string pdfPath = "intermediate.pdf";
        const string thumbnailPath = "thumbnail.png";

        // Ensure the XML source exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Load the XML and convert it to a PDF document.
        //    XmlLoadOptions is required for XML → PDF conversion.
        // --------------------------------------------------------------------
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Save the intermediate PDF (optional, can be omitted if not needed later).
            pdfDoc.Save(pdfPath);

            // ----------------------------------------------------------------
            // 2. Create a ThumbnailDevice.
            //    The default constructor creates a 200x200 pixel thumbnail.
            //    You can specify custom dimensions, e.g. new ThumbnailDevice(150, 150).
            // ----------------------------------------------------------------
            ThumbnailDevice thumbDevice = new ThumbnailDevice(); // No using – ThumbnailDevice does not implement IDisposable

            // The first page in Aspose.Pdf is indexed at 1.
            Page firstPage = pdfDoc.Pages[1];

            // Create the output stream for the PNG thumbnail.
            using (FileStream outStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
            {
                // Process the page and write the thumbnail PNG to the stream.
                thumbDevice.Process(firstPage, outStream);
            }
        }

        Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
    }
}
